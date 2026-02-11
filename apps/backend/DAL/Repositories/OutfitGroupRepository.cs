using DAL.Context;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class OutfitGroupRepository : GenericRepository<OutfitGroup>, IOutfitGroupRepository
{
    public OutfitGroupRepository(WardrobeDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<OutfitGroup>> GetAllByUserIdAsync(int userId)
    {
        return await context.OutfitGroups
            .Where(g => g.UserID == userId)
            .Include(g => g.OutfitGroups)
            .ToListAsync();
    }
}