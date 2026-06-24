using AutoMapper;
using BLL.DTO.Publication;
using BLL.DTO.Tag;
using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Helpers;
using DAL.Helpers.Params;
using DAL.Repositories.Interfaces;
using System.Text;
using System.Text.Json;
using System.Net.Http;

namespace BLL.Services;

public class PublicationService : IPublicationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ITagService _tagService;
    private readonly IHttpClientFactory _httpClientFactory;

    public PublicationService(IUnitOfWork unitOfWork, IMapper mapper, ITagService tagService, IHttpClientFactory httpClientFactory)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _tagService = tagService;
        _httpClientFactory = httpClientFactory;
    }

    public PagedList<PublicationListDto> GetAllPublication(PublicationParams parameters)
    {
        var publication = _unitOfWork.Publications.GetAllPublication(parameters);
        return _mapper.Map<PagedList<PublicationListDto>>(publication);
    }

    public async Task<PublicationDetailsDto?> GetPublicationDetailsAsync(int id)
    {
        var publication = await _unitOfWork.Publications.GetPublicationDetailsAsync(id);
        return _mapper.Map<PublicationDetailsDto?>(publication);
    }

    public async Task<PagedList<PublicationListDto>> GetByUserAsync(int userId, PublicationParams parameters)
    {
        var publication = await _unitOfWork.Publications.GetByUserAsync(userId, parameters);
        return _mapper.Map<PagedList<PublicationListDto>>(publication);
    }

    public async Task<int> CreatePublicationAsync(PublicationDto dto, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var publication = _mapper.Map<Publication>(dto);

            await _unitOfWork.Publications.AddAsync(publication);
            await _unitOfWork.SaveAsync();
            
            var publicationTags = new List<PublicationTag>();
            foreach (var tagName in dto.Tags.Distinct())
            {
                var tagId = await _tagService.AddTagAsync(new CreateTagDto { TagName = tagName }, cancellationToken);

                publicationTags.Add(new PublicationTag
                {
                    PublicationID = publication.Id,
                    TagID = tagId
                });
            }
            
            await _unitOfWork.PublicationTags.AddRangeAsync(publicationTags);
            await _unitOfWork.SaveAsync();
            
            var outfitDetails = await _unitOfWork.Outfits.GetOutfitDetailsAsync(publication.OutfitID);
            if (outfitDetails != null && outfitDetails.Items != null && outfitDetails.Items.Any())
            {
                var indexPayload = outfitDetails.Items.Select(oi => new
                {
                    outfit_id = publication.OutfitID,
                    item_id = oi.ClothingItemID,
                    image_url = oi.ClothingItem?.ImageURL ?? ""
                }).Where(p => !string.IsNullOrEmpty(p.image_url)).ToList();

                if (indexPayload.Any())
                {
                    _ = Task.Run(async () =>
                    {
                        try
                        {
                            Console.WriteLine($"[C# BACKGROUND SYSTEM] Відправка нового образу {publication.OutfitID} на ШІ індексацію...");
                            var client = _httpClientFactory.CreateClient();
                            var json = JsonSerializer.Serialize(indexPayload);
                            var content = new StringContent(json, Encoding.UTF8, "application/json");
                            
                            var response = await client.PostAsync("http://smart-wardrobe.ml.visual-search:8003/index-outfits", content);
                            Console.WriteLine($"[C# BACKGROUND SYSTEM] Результат індексації: {response.StatusCode}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"[C# BACKGROUND ERROR] Помилка відправки в ML сервіс: {ex.Message}");
                        }
                    }, cancellationToken);
                }
            }
            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            return publication.Id;
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
    
    public async Task<IEnumerable<PublicationListDto>> SearchPublicationsBySimilarItemAsync(int itemId, CancellationToken cancellationToken)
    {
        Console.WriteLine($"\n[C# INVESTIGATION] Початок пошуку образів для речі ID: {itemId}");
        
        var targetItem = await _unitOfWork.ClothingItems.GetByIdAsync(itemId);
        if (targetItem == null || string.IsNullOrEmpty(targetItem.ImageURL))
        {
            Console.WriteLine($"[C# INVESTIGATION ERROR] Річ з ID {itemId} не знайдена в базі даних або не має ImageURL!");
            return new List<PublicationListDto>();
        }
        
        var client = _httpClientFactory.CreateClient();
        var searchPayload = new { image_url = targetItem.ImageURL, top_k = 10 };
        
        var jsonPayload = JsonSerializer.Serialize(searchPayload);
        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("http://smart-wardrobe.ml.visual-search:8003/search-similar-outfits", content, cancellationToken);
        
        if (!response.IsSuccessStatusCode) return new List<PublicationListDto>();

        var jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);
        var mlResult = JsonSerializer.Deserialize<PythonVisualSearchResponse>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (mlResult == null || mlResult.Data == null || !mlResult.Data.Any()) return new List<PublicationListDto>();
        
        var matchedOutfitIds = mlResult.Data.Select(d => d.OutfitId).Distinct().ToList();
        var allPublications = _unitOfWork.Publications.GetAllPublication(new PublicationParams { PageNumber = 1, PageSize = 100 });

        var filteredPublications = allPublications
            .Where(p => matchedOutfitIds.Contains(p.OutfitID))
            .OrderBy(p => matchedOutfitIds.IndexOf(p.OutfitID))
            .ToList();

        return _mapper.Map<IEnumerable<PublicationListDto>>(filteredPublications);
    }

    public async Task UpdatePublicationAsync(int id, UpdatePublicationDto publicationDto, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var publication = await _unitOfWork.Publications.GetByIdAsync(id);
            if (publication == null) throw new KeyNotFoundException("Publication not found.");

            _mapper.Map(publicationDto, publication);
            await _unitOfWork.Publications.UpdateAsync(publication);
            
            var existingTags = await _unitOfWork.PublicationTags.GetTagsByPublicationIdAsync(publication.Id);
            var existingTagNames = existingTags.Select(t => t.Tag.TagName).ToList();
            var newTagNames = publicationDto.Tags.Distinct().ToList();
            
            var tagsToAdd = newTagNames.Except(existingTagNames).ToList();
            var tagsToRemove = existingTagNames.Except(newTagNames).ToList();
            
            foreach (var tagName in tagsToAdd)
            {
                var tagId = await _tagService.AddTagAsync(new CreateTagDto { TagName = tagName }, cancellationToken);
                await _unitOfWork.PublicationTags.AddAsync(new PublicationTag { PublicationID = publication.Id, TagID = tagId });
            }
            
            foreach (var tagName in tagsToRemove)
            {
                var tag = await _unitOfWork.Tags.GetByNameAsync(tagName);
                if (tag != null)
                {
                    await _unitOfWork.PublicationTags.DeleteByPublicationAndTagAsync(publication.Id, tag.Id);
                }
            }

            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitTransactionAsync(cancellationToken);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }

    public async Task DeletePublicationAsync(int id, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var publication = await _unitOfWork.Publications.GetByIdAsync(id);
            if (publication == null) throw new KeyNotFoundException("publication not found.");

            int outfitIdToDelete = publication.OutfitID;

            await _unitOfWork.Publications.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
        
            _ = Task.Run(async () =>
            {
                try
                {
                    var client = _httpClientFactory.CreateClient();
                    await client.DeleteAsync($"http://smart-wardrobe.ml.visual-search:8003/delete-outfit/{outfitIdToDelete}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[C# BACKGROUND ERROR] Не вдалося видалити образ з ML індексу: {ex.Message}");
                }
            }, cancellationToken);

            await _unitOfWork.CommitTransactionAsync(cancellationToken);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
    
    public async Task<PagedList<PublicationListDto>> GetFollowingsPublicationsAsync(int userId, PublicationParams parameters)
    {
        var publications = await _unitOfWork.Publications.GetPublicationsOfFollowingsAsync(userId, parameters);
        return _mapper.Map<PagedList<PublicationListDto>>(publications);
    }
    
    public async Task<PagedList<PublicationListDto>> GetSavedPublicationsAsync(int userId, PublicationParams parameters)
    {
        var publications = await _unitOfWork.Publications.GetSavedPublicationsAsync(userId, parameters);
        return _mapper.Map<PagedList<PublicationListDto>>(publications);
    }
    
    public async Task<bool> SyncExistingDatasetWithMlAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Синхронізація взаємодій користувачів з ШІ...");

        try
        {
            var likesLog = await _unitOfWork.Publications.GetLikesInteractionLogAsync();
            var savesLog = await _unitOfWork.Publications.GetSavesInteractionLogAsync();

            var fullInteractionsDataset = likesLog.Concat(savesLog).ToList();

            if (!fullInteractionsDataset.Any())
            {
                Console.WriteLine("Немає взаємодій для навчання.");
                return false;
            }

            var client = _httpClientFactory.CreateClient();
            var json = JsonSerializer.Serialize(fullInteractionsDataset);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://smart-wardrobe.ml.visual-search:8003/api/recommend/train", content, cancellationToken);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[C# DATA SYNC CRITICAL ERROR] Помилка: {ex.Message}");
            return false;
        }
    }
}