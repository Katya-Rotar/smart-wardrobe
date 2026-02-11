using DAL.Entities;
using DAL.Helpers;
using DAL.Helpers.Params;

namespace DAL.Repositories.Interfaces;

public interface IClothingItemRepository : IGenericRepository<ClothingItem>
{
    PagedList<ClothingItem> GetAllClothingItems(ClothingItemParams parameters, IEnumerable<string> searchFields);
    
    Task<Dictionary<string, List<ClothingItem>>> GetClothingItemsGroupedByTypeAsync(int userId);
    
    Task<PagedList<ClothingItem>> GetByTypeAsync(int typeId, int userId, ClothingItemParams parameters);
    
    Task<ClothingItem?> GetClothingItemDetailsAsync(int id);
}