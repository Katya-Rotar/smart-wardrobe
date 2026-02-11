using DAL.Entities;

namespace DAL.Repositories.Interfaces;

public interface IOutfitGroupRepository : IGenericRepository<OutfitGroup>
{
    Task<IEnumerable<OutfitGroup>> GetAllByUserIdAsync(int userId);
}