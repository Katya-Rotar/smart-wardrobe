using AutoMapper;
using BLL.DTO.User;
using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interfaces;

namespace BLL.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UserDto> GetUserByIdAsync(int id)
    {
        return _mapper.Map<UserDto>(await _unitOfWork.Users.GetByIdAsync(id));
    }

    public async Task<int> AddUserAsync(CreateUserDto userDto, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var user = _mapper.Map<User>(userDto);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.PasswordHash);
            user.Role = "User";
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitTransactionAsync(cancellationToken);
            return user.Id;
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }

    public async Task UpdateUserAsync(UpdateUserDto userDto, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var existingUser = await _unitOfWork.Users.GetByIdAsync(userDto.Id);
            _mapper.Map(userDto, existingUser);
            if (!string.IsNullOrEmpty(userDto.PasswordHash))
            {
                existingUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.PasswordHash);
            }
            await _unitOfWork.Users.UpdateAsync(existingUser);
            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitTransactionAsync(cancellationToken);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }

    public async Task DeleteUserAsync(int id, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            await _unitOfWork.Users.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitTransactionAsync(cancellationToken);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
    
    public async Task<UserDto?> AuthenticateAsync(LoginDto login)
    {
        var user = await _unitOfWork.Users.AuthenticateAsync(login.Email);
        
        if (user == null || !BCrypt.Net.BCrypt.Verify(login.Password, user.PasswordHash))
        {
            return null;
        }
        
        return _mapper.Map<UserDto>(user);
    }
}