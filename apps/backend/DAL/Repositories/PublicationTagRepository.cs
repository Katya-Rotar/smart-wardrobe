using DAL.Context;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class PublicationTagRepository : GenericRepository<PublicationTag>, IPublicationTagRepository
{
    public PublicationTagRepository(WardrobeDbContext context) : base(context)
    {
    }
    
    public async Task<IEnumerable<PublicationTag>> GetTagsByPublicationIdAsync(int publicationId)
    {
        return await context.Set<PublicationTag>()
            .Include(pt => pt.Tag)
            .Where(pt => pt.PublicationID == publicationId)
            .ToListAsync();
    }

    public async Task DeleteByPublicationAndTagAsync(int publicationId, int tagId)
    {
        var relation = await context.Set<PublicationTag>()
            .FirstOrDefaultAsync(pt => pt.PublicationID == publicationId && pt.TagID == tagId);
        if (relation != null)
        {
            context.Set<PublicationTag>().Remove(relation);
        }
    }

}
