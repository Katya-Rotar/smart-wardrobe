using DAL.Context;
using DAL.Entities;
using DAL.Helpers.Params;
using DAL.Helpers.Search;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Interfaces;

public class TagRepository : GenericRepository<Tag>, ITagRepository
{
    public TagRepository(WardrobeDbContext context) : base(context)
    {
    }

    public async Task<Tag?> GetByNameAsync(string name)
    {
        return await context.Set<Tag>().FirstOrDefaultAsync(t => t.TagName== name);
    }
}