using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entities;
using BLL.DTO;

namespace BLL.Services.Interfaces;

public interface IRecommendationService
{
    Task<List<ClothingItemAllDto>> GetRecommendedItemsAsync(int userId, float temp, int weatherCode, int styleId);
    Task<IEnumerable<PublicationListDto>> GetSmartRecommendationsAsync(int currentUserId, int currentPublicationId, CancellationToken cancellationToken);
    Task SyncInteractionsWithMlAsync(CancellationToken cancellationToken);
}