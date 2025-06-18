using DAL.Entities;
using DAL.Helpers.Params;

namespace DAL.Repositories.Interfaces;

public interface ITagRepository : IGenericRepository<Tag>
{
    Task<Tag?> GetByNameAsync(string name);
    public Task<List<Tag>> SearchTagsByNameAsync(string query);
}