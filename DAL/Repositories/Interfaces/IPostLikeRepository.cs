using DAL.Entities;

namespace DAL.Repositories.Interfaces;

public interface IPostLikeRepository : IGenericRepository<PostLike>
{
    Task<bool> IsPostLikedAsync(int userId, int publicationId);
    Task<PostLike?> GetByUserAndPublicationAsync(int userId, int publicationId);
    Task<int> GetLikesCountAsync(int publicationId);
}