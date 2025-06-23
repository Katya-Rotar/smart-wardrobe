using AutoMapper;
using BLL.DTO.Follower;
using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interfaces;

namespace BLL.Services;

public class FollowerService : IFollowerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public FollowerService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int> AddFollowerAsync(FollowerDto dto, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var isFollowing = await IsFollowingAsync(dto);
            if (isFollowing)
                throw new InvalidOperationException("Already signed.");

            var follower = _mapper.Map<Follower>(dto);
            follower.CreatedAt = DateTime.UtcNow;

            await _unitOfWork.Followers.AddAsync(follower);
            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            return follower.Id;
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }

    public async Task DeleteAsync(FollowerDto dto, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var followerId = await _unitOfWork.Followers
                .GetFollowerRelationIdAsync(dto.FollowerID, dto.FollowingID);

            if (followerId == null)
                throw new KeyNotFoundException("Підписка не знайдена.");

            await _unitOfWork.Followers.DeleteAsync(followerId.Value);
            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitTransactionAsync(cancellationToken);
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }

    public async Task<bool> IsFollowingAsync(FollowerDto dto)
    {
        return await _unitOfWork.Followers.IsFollowingAsync(dto.FollowerID, dto.FollowingID);
    }

    public async Task<int> GetFollowingCountAsync(int userId)
    {
        return await _unitOfWork.Followers.GetFollowingCountAsync(userId);
    }

    public async Task<int> GetFollowerCountAsync(int userId)
    {
        return await _unitOfWork.Followers.GetFollowerCountAsync(userId);
    }

    public async Task<List<UserShortDto>> GetFollowingUsersAsync(int userId)
    {
        var users = await _unitOfWork.Followers.GetFollowingUsersAsync(userId);
        return _mapper.Map<List<UserShortDto>>(users);
    }

    public async Task<List<UserShortDto>> GetFollowerUsersAsync(int userId)
    {
        var users = await _unitOfWork.Followers.GetFollowerUsersAsync(userId);
        return _mapper.Map<List<UserShortDto>>(users);
    }
}