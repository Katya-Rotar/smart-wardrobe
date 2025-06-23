using DAL.Context;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class FollowerRepository : GenericRepository<Follower>, IFollowerRepository
{
    public FollowerRepository(WardrobeDbContext context) : base(context)
    {
    }

    public async Task<bool> IsFollowingAsync(int followerId, int followingId)
    {
        return await context.Followers
            .AnyAsync(f => f.FollowerID == followerId && f.FollowingID == followingId);
    }

    public async Task<int> GetFollowingCountAsync(int userId)
    {
        return await context.Followers.CountAsync(f => f.FollowerID == userId);
    }

    public async Task<int> GetFollowerCountAsync(int userId)
    {
        return await context.Followers.CountAsync(f => f.FollowingID == userId);
    }
    
    public async Task<List<User>> GetFollowingUsersAsync(int userId)
    {
        return await context.Followers
            .Where(f => f.FollowerID == userId)
            .Include(f => f.FollowingProfile)
            .Select(f => f.FollowingProfile!)
            .ToListAsync();
    }

    public async Task<List<User>> GetFollowerUsersAsync(int userId)
    {
        return await context.Followers
            .Where(f => f.FollowingID == userId)
            .Include(f => f.FollowerProfile)
            .Select(f => f.FollowerProfile!)
            .ToListAsync();
    }

    public async Task<int?> GetFollowerRelationIdAsync(int followerId, int followingId)
    {
        return await context.Followers
            .Where(f => f.FollowerID == followerId && f.FollowingID == followingId)
            .Select(f => (int?)f.Id)
            .FirstOrDefaultAsync();
    }
}