using BLL.DTO.Publication;
using DAL.Entities;

namespace BLL.Services.Interfaces;
using BLL.DTO.Publication;
using DAL.Helpers;
using DAL.Helpers.Params;

namespace BLL.Services.Interfaces
{
    public interface IPublicationService
    {
        PagedList<PublicationListDto> GetAllPublication(PublicationParams parameters);
        Task<PublicationDetailsDto?> GetPublicationDetailsAsync(int id);
        Task<PagedList<PublicationListDto>> GetByUserAsync(int userId, PublicationParams parameters);
        Task<int> CreatePublicationAsync(PublicationDto dto, CancellationToken cancellationToken);
        Task UpdatePublicationAsync(int id, UpdatePublicationDto publicationDto, CancellationToken cancellationToken);
        Task DeletePublicationAsync(int id, CancellationToken cancellationToken);
        Task<PagedList<PublicationListDto>> GetFollowingsPublicationsAsync(int userId, PublicationParams parameters);
        Task<PagedList<PublicationListDto>> GetSavedPublicationsAsync(int userId, PublicationParams parameters);
        
        // ШІ №1: Пошук за фото
        Task<IEnumerable<PublicationListDto>> SearchPublicationsBySimilarItemAsync(int itemId, CancellationToken cancellationToken);
        
        // ШІ №2: Колаборативні рекомендації соціалки за лайками
        Task<IEnumerable<PublicationListDto>> GetSocialRecommendationsAsync(int publicationId, int currentUserId, CancellationToken cancellationToken);
        
        // Синхронізація логів
        Task<bool> SyncExistingDatasetWithMlAsync(CancellationToken cancellationToken);
    }
}