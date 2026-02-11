using BLL.Services.Interfaces;
using BLL.DTO.User;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly string _key = "W8zDp4x2mY9vK6nF3qR7tW5eX2aZ7pU6sQ9bJ4vL2cT8nR5oX3kV6rP7mY2qJ9";

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        return Ok(user);
    }
    
    [Authorize]
    [HttpGet("user-info")]
    public async Task<IActionResult> GetUserById()
    {
        var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userIdFromToken != null && int.TryParse(userIdFromToken, out var userId))
        {
            var user = await _userService.GetUserByIdAsync(userId);
            return Ok(user);
        }

        return Unauthorized("You are not authorized to view this user information.");
    }

    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] CreateUserDto userDto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = await _userService.AddUserAsync(userDto, cancellationToken);
        return CreatedAtAction(nameof(GetUserById), new { id = userId }, userDto);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto userDto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (id != userDto.Id)
            return BadRequest("User ID mismatch.");

        await _userService.UpdateUserAsync(userDto, cancellationToken);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id, CancellationToken cancellationToken)
    {
        await _userService.DeleteUserAsync(id, cancellationToken);
        return NoContent();
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto request)
    {
        var user = await _userService.AuthenticateAsync(request);
        if (user != null)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_key);
        
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, request.Email),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
        
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);

            return Ok(new { Token = jwt });
        }

        return Unauthorized("Invalid credentials");
    }
    
    [Authorize]
    [HttpGet("user-id-from-token")]
    public IActionResult GetUserIdFromToken()
    {
        var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userIdFromToken != null)
        {
            return Ok(new { UserId = userIdFromToken });
        }

        return Unauthorized("Invalid token.");
    }
}