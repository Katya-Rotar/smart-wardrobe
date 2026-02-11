namespace BLL.DTO.User;

public class CreateUserDto
{
    public string Username { get; set; }

    public string? ProfileImage { get; set; }

    public string PasswordHash { get; set; }

    public string Email { get; set; }
}