using AutoMapper;
using BLL.DTO;
using BLL.DTO.ML;
using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;

namespace BLL.Services;

public class RecommendationService : IRecommendationService
{
    private readonly HttpClient _httpClient;
    private readonly IClothingItemRepository _repository;
    private readonly IPublicationRepository _publicationRepository;
    private readonly IMapper _mapper;
    private readonly string _mlUrl;
    private readonly string _mlSimilarUrl; 
    private readonly string _mlTrainUrl;

    public RecommendationService(
        HttpClient httpClient, 
        IClothingItemRepository repository, 
        IPublicationRepository publicationRepository, // Додаємо ін'єкцію репозиторію публікацій
        IConfiguration config,
        IMapper mapper) 
    {
        _httpClient = httpClient;
        _repository = repository;
        _publicationRepository = publicationRepository;
        _mapper = mapper;
        
        // Старий ендпоінт для підбору одягу по погоді
        _mlUrl = config["MLServices:RecommendationUrl"] ?? "";
        
        // Нові ендпоінти для соціалки (пропиши їх у appsettings.json)
        _mlSimilarUrl = config["MLServices:SimilarPublicationsUrl"] ?? "http://ml-recommendation-service:8003/api/recommend/similar-publications";
        _mlTrainUrl = config["MLServices:TrainUrl"] ?? "http://ml-recommendation-service:8003/api/recommend/train";
    }
    
    public async Task<List<ClothingItemAllDto>> GetRecommendedItemsAsync(int userId, float temp, int weatherCode, int styleId)
    {
        var items = await _repository.GetByUserIdAsync(userId);

        var request = new MlRecommendationRequest
        {
            Temperature = temp,
            WeatherCode = weatherCode,
            StyleId = styleId,
            Items = items.Select(i => new MlItemDto
            {
                Id = i.Id,
                TypeID = i.TypeID,
                SeasonIds = i.Seasons.Select(s => s.SeasonID).ToList(),
                StyleIds = i.Styles.Select(st => st.StyleID).ToList()
            }).ToList()
        };

        var response = await _httpClient.PostAsJsonAsync(_mlUrl, request);
        
        if (!response.IsSuccessStatusCode) 
        {
            return _mapper.Map<List<ClothingItemAllDto>>(items);
        }

        var scoredItems = await response.Content.ReadFromJsonAsync<List<MlRecommendationResponse>>();

        var sortedEntities = items
            .OrderByDescending(i => {
                var scoreItem = scoredItems?.FirstOrDefault(s => s.Id == i.Id);
                return scoreItem?.Score ?? -100;
            })
            .ToList();

        return _mapper.Map<List<ClothingItemAllDto>>(sortedEntities);
    }
    
    public async Task<List<PublicationListDto>> GetSmartRecommendationsAsync(int currentUserId, int currentPublicationId, CancellationToken cancellationToken)
    {
        // Сценарій А: Користувач неавторизований (гість) -> повертаємо тренди/свіжі пости
        if (currentUserId <= 0)
        {
            return await GetTrendingPublicationsAsync();
        }

        // Сценарій Б: Користувач авторизований -> запитуємо схожість у Python ШІ
        var searchPayload = new { publicationId = currentPublicationId, top_k = 15 };
        
        try
        {
            var response = await _httpClient.PostAsJsonAsync(_mlSimilarUrl, searchPayload, cancellationToken);
            
            if (!response.IsSuccessStatusCode)
            {
                return await GetTrendingPublicationsAsync(); // Якщо ШІ впав — показуємо тренди
            }

            // Використовуємо твою DTO-структуру PythonVisualSearchResponse
            var mlResult = await response.Content.ReadFromJsonAsync<PythonVisualSearchResponse>(
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }, 
                cancellationToken
            );

            if (mlResult == null || mlResult.Data == null || !mlResult.Data.Any())
            {
                return await GetTrendingPublicationsAsync(); // Холодний старт образу -> показуємо тренди
            }

            // Витягуємо ID у порядку релевантності від ШІ [81, 83, 74...]
            var matchedOutfitIds = mlResult.Data.Select(d => d.OutfitId).Distinct().ToList();

            // Дістаємо ці публікації з бази даних
            var dbPublications = await _publicationRepository.GetByIdsAsync(matchedOutfitIds);
            
            // СОРТУВАННЯ: Примусово виставляємо їх у чергу так, як хотів Python ШІ
            var sortedPublications = dbPublications
                .OrderBy(p => matchedOutfitIds.IndexOf(p.Id)) 
                .ToList();

            // Мапимо у вихідні DTO для фронтенду
            return _mapper.Map<List<PublicationListDto>>(sortedPublications);
        }
        catch
        {
            // У разі будь-яких критичних помилок мережі страхуємося трендами
            return await GetTrendingPublicationsAsync();
        }
    }
    
    public async Task SyncInteractionsWithMlAsync(CancellationToken cancellationToken)
    {
        // Витягуємо лог взаємодій із твоїх репозиторіїв
        // (Тут підстав назви методів, які є у твоїх репозиторіях для Likes/Saves/Comments)
        var likes = await _repository.GetTargetLikesLogAsync(); 
        var saves = await _repository.GetTargetSavesLogAsync();
        var comments = await _repository.GetTargetCommentsLogAsync();

        // Формуємо плоский список взаємодій із вагою дій, як у лабі
        var interactions = likes.Select(l => new InteractionDto { UserId = l.UserId, ItemId = l.PublicationId, Weight = 3 })
            .Concat(saves.Select(s => new InteractionDto { UserId = s.UserId, ItemId = s.PublicationId, Weight = 5 }))
            .Concat(comments.Select(c => new InteractionDto { UserId = c.UserId, ItemId = c.PublicationId, Weight = 4 }))
            .ToList();

        // Відправляємо масив на FastAPI `/train`
        await _httpClient.PostAsJsonAsync(_mlTrainUrl, interactions, cancellationToken);
    }
    
    private async Task<List<PublicationListDto>> GetTrendingPublicationsAsync()
    {
        // Беремо, наприклад, останні 15 свіжих постів з бази
        var publications = await _publicationRepository.GetRecentPublicationsAsync(15);
        return _mapper.Map<List<PublicationListDto>>(publications);
    }
} 