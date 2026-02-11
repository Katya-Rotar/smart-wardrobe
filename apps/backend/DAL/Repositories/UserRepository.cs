using DAL.Context;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(WardrobeDbContext context) : base(context)
    {
    }
    
    public async Task<User> AuthenticateAsync(string email)
    {
        return await context.Users.FirstOrDefaultAsync(x => x.Email == email);
    }
}