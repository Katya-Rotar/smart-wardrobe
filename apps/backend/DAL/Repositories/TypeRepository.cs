using DAL.Context;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Type = DAL.Entities.Type;

namespace DAL.Repositories;

public class TypeRepository : GenericRepository<Type>, ITypeRepository
{
    public TypeRepository(WardrobeDbContext context) : base(context)
    {
    }
    
    public async Task<IEnumerable<Type>> GetTypesByCategoryIdAsync(int categoryId)
    {
        return await context.TypeCategories
            .Where(tc => tc.CategoryID == categoryId)
            .Select(tc => tc.Type!)
            .Distinct()
            .ToListAsync();
    }
}