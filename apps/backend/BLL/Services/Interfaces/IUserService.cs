using BLL.DTO.User;

namespace BLL.Services.Interfaces;

public interface IUserService
{
    Task<UserDto> GetUserByIdAsync(int id);
    Task<int> AddUserAsync(CreateUserDto userDto, CancellationToken cancellationToken);
    Task UpdateUserAsync(UpdateUserDto userDto, CancellationToken cancellationToken);
    Task DeleteUserAsync(int id, CancellationToken cancellationToken);
    Task<UserDto?> AuthenticateAsync(LoginDto login);
}