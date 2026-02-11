using DAL.Entities;
using DAL.Helpers;
using DAL.Helpers.Params;

namespace DAL.Repositories.Interfaces;

public interface IOutfitRepository : IGenericRepository<Outfit>
{
    PagedList<Outfit> GetAllOutfit(OutfitParams parameters);
    Task<Outfit?> GetOutfitDetailsAsync(int id);
    Task<IEnumerable<Outfit>> GetOutfitsByItemIdAsync(int itemId);
}