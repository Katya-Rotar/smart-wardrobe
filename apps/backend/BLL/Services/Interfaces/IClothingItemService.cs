using BLL.DTO;
using DAL.Entities;
using DAL.Helpers;
using DAL.Helpers.Params;

namespace BLL.Services.Interfaces;

public interface IClothingItemService
{
    Task<ClothingItemDto> GetClothingItemByIdAsync(int id);
    Task<int> AddClothingItemAsync(CreateClothingItemDto clothingItemDto, CancellationToken cancellationToken);
    Task UpdateClothingItemAsync(ClothingItemDto clothingItemDto, CancellationToken cancellationToken);
    Task DeleteClothingItemAsync(int id, CancellationToken cancellationToken);

    Task<PagedList<ClothingItemDto>> GetItemsByTypeAsync(int typeId, int userId, ClothingItemParams parameters);
    PagedList<ClothingItemAllDto> GetAllClothingItems(ClothingItemParams parameters);
    Task<Dictionary<string, List<ClothingItemDto>>> GetClothingItemsGroupedByTypeAsync(int userId);
    Task<ClothingItemAllDto> GetClothingItemDetailsAsync(int id);
}