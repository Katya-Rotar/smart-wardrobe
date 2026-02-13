using DAL.Context;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class PostLikeRepository : GenericRepository<PostLike>, IPostLikeRepository
{
    public PostLikeRepository(WardrobeDbContext context) : base(context)
    {
        
    }

    public async Task<bool> IsPostLikedAsync(int userId, int publicationId)
    {
        return await context.PostLikes
            .AnyAsync(p => p.UserID == userId && p.PublicationID == publicationId);
    }

    public async Task<PostLike?> GetByUserAndPublicationAsync(int userId, int publicationId)
    {
        return await context.PostLikes
            .FirstOrDefaultAsync(p => p.UserID == userId && p.PublicationID == publicationId);
    }

    public async Task<int> GetLikesCountAsync(int publicationId)
    {
        return await context.PostLikes
            .CountAsync(p => p.PublicationID == publicationId);
    }
}
