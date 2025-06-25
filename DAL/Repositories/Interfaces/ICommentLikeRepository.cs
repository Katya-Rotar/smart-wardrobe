using DAL.Entities;

namespace DAL.Repositories.Interfaces;

public interface ICommentLikeRepository : IGenericRepository<CommentLike>
{
    Task<bool> IsCommentLikedAsync(int userId, int commentId);
    Task<CommentLike?> GetByUserAndCommentAsync(int userId, int commentId);
    Task<int> GetLikesCountAsync(int commentId);
}