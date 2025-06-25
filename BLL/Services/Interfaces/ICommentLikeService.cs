using BLL.DTO.CommentLike;

namespace BLL.Services.Interfaces;

public interface ICommentLikeService
{
    Task<int> AddLikeAsync(CommentLikeDto dto, CancellationToken cancellationToken);
    Task RemoveLikeAsync(CommentLikeDto dto, CancellationToken cancellationToken);
    Task<bool> IsCommentLikedAsync(CommentLikeDto dto);
    Task<int> GetLikesCountAsync(int commentId);
}
