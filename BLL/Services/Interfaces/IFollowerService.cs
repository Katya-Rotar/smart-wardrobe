using BLL.DTO.Follower;

namespace BLL.Services.Interfaces;

public interface IFollowerService
{
    Task<int> AddFollowerAsync(FollowerDto followerDto, CancellationToken cancellationToken);
    Task DeleteAsync(FollowerDto dto, CancellationToken cancellationToken);
    Task<bool> IsFollowingAsync(FollowerDto followerDto);
    Task<int> GetFollowingCountAsync(int userId);
    Task<int> GetFollowerCountAsync(int userId);
    Task<List<UserShortDto>> GetFollowingUsersAsync(int userId);
    Task<List<UserShortDto>> GetFollowerUsersAsync(int userId);
}