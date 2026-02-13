using BLL.DTO.PostLike;

namespace BLL.Services.Interfaces;

public interface IPostLikeService
{
    Task<int> AddLikeAsync(PostLikeDto dto, CancellationToken cancellationToken);
    Task RemoveLikeAsync(PostLikeDto dto, CancellationToken cancellationToken);
    Task<bool> IsPostLikedAsync(PostLikeDto dto);
    Task<int> GetLikesCountAsync(int publicationId);
}
