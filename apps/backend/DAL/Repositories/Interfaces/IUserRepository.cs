using DAL.Entities;

namespace DAL.Repositories.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User> AuthenticateAsync(string email);
}