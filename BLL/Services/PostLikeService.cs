using AutoMapper;
using BLL.DTO.PostLike;
using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interfaces;

namespace BLL.Services;

public class PostLikeService : IPostLikeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PostLikeService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int> AddLikeAsync(PostLikeDto dto, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var isLiked = await IsPostLikedAsync(dto);
            if (isLiked)
                throw new InvalidOperationException("Already liked.");

            var postLike = _mapper.Map<PostLike>(dto);
            await _unitOfWork.PostLikes.AddAsync(postLike);
            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            return postLike.Id;
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }

    public async Task RemoveLikeAsync(PostLikeDto dto, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var postLike = await _unitOfWork.PostLikes.GetByUserAndPublicationAsync(dto.UserID, dto.PublicationID);
            if (postLike == null)
                throw new KeyNotFoundException("Like not found.");

            await _unitOfWork.PostLikes.DeleteAsync(postLike.Id);
            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitTransactionAsync(cancellationToken);
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }

    public async Task<bool> IsPostLikedAsync(PostLikeDto dto)
    {
        return await _unitOfWork.PostLikes.IsPostLikedAsync(dto.UserID, dto.PublicationID);
    }

    public async Task<int> GetLikesCountAsync(int publicationId)
    {
        return await _unitOfWork.PostLikes.GetLikesCountAsync(publicationId);
    }
}
