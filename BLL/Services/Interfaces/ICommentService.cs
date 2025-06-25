using BLL.DTO.Comment;

namespace BLL.Services.Interfaces;

public interface ICommentService
{
    Task<int> AddCommentAsync(CreateCommentDto dto, CancellationToken cancellationToken);
    Task<List<CommentDto>> GetCommentsByPublicationAsync(int publicationId);
}
