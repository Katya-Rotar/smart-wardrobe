using DAL.Context;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class CommentLikeRepository : GenericRepository<CommentLike>, ICommentLikeRepository
{
    public CommentLikeRepository(WardrobeDbContext context) : base(context) { }

    public async Task<bool> IsCommentLikedAsync(int userId, int commentId)
    {
        return await context.CommentLikes.AnyAsync(cl => cl.UserID == userId && cl.CommentID == commentId);
    }

    public async Task<CommentLike?> GetByUserAndCommentAsync(int userId, int commentId)
    {
        return await context.CommentLikes
            .FirstOrDefaultAsync(cl => cl.UserID == userId && cl.CommentID == commentId);
    }

    public async Task<int> GetLikesCountAsync(int commentId)
    {
        return await context.CommentLikes.CountAsync(cl => cl.CommentID == commentId);
    }
}
