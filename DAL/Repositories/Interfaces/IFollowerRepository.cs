using DAL.Entities;

namespace DAL.Repositories.Interfaces;

public interface IFollowerRepository : IGenericRepository<Follower>
{
    Task<bool> IsFollowingAsync(int followerId, int followingId);
    Task<int> GetFollowingCountAsync(int userId);
    Task<int> GetFollowerCountAsync(int userId);
    Task<List<User>> GetFollowingUsersAsync(int userId);
    Task<List<User>> GetFollowerUsersAsync(int userId);
    Task<int?> GetFollowerRelationIdAsync(int followerId, int followingId);
}