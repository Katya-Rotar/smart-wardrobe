using Type = DAL.Entities.Type;

namespace DAL.Repositories.Interfaces;

public interface ITypeRepository : IGenericRepository<Type>
{
    Task<IEnumerable<Type>> GetTypesByCategoryIdAsync(int categoryId);
}