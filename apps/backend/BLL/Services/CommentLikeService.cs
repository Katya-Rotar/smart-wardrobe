using AutoMapper;
using BLL.DTO.CommentLike;
using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interfaces;

namespace BLL.Services;

public class CommentLikeService : ICommentLikeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CommentLikeService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int> AddLikeAsync(CommentLikeDto dto, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var alreadyLiked = await _unitOfWork.CommentLikes.IsCommentLikedAsync(dto.UserID, dto.CommentID);
            if (alreadyLiked)
                throw new InvalidOperationException("Comment already liked.");

            var like = _mapper.Map<CommentLike>(dto);
            await _unitOfWork.CommentLikes.AddAsync(like);
            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            return like.Id;
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }

    public async Task RemoveLikeAsync(CommentLikeDto dto, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var like = await _unitOfWork.CommentLikes.GetByUserAndCommentAsync(dto.UserID, dto.CommentID);
            if (like == null)
                throw new KeyNotFoundException("Comment like not found.");

            await _unitOfWork.CommentLikes.DeleteAsync(like.Id);
            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitTransactionAsync(cancellationToken);
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }

    public async Task<bool> IsCommentLikedAsync(CommentLikeDto dto)
    {
        return await _unitOfWork.CommentLikes.IsCommentLikedAsync(dto.UserID, dto.CommentID);
    }

    public async Task<int> GetLikesCountAsync(int commentId)
    {
        return await _unitOfWork.CommentLikes.GetLikesCountAsync(commentId);
    }
}
