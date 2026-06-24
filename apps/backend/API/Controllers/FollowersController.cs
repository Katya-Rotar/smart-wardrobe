using System.Security.Claims;
using BLL.DTO.Follower;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FollowersController : ControllerBase
    {
        private readonly IFollowerService _followerService;

        public FollowersController(IFollowerService followerService)
        {
            _followerService = followerService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Follow([FromBody] FollowerDto dto, CancellationToken cancellationToken)
        {
            var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdFromToken == null || !int.TryParse(userIdFromToken, out var userId))
                return Unauthorized("Invalid token.");

            dto.FollowerID = userId;
            var id = await _followerService.AddFollowerAsync(dto, cancellationToken);
            return Ok(new { id });
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Unfollow([FromBody] FollowerDto dto, CancellationToken cancellationToken)
        {
            var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdFromToken == null || !int.TryParse(userIdFromToken, out var userId))
                return Unauthorized("Invalid token.");

            dto.FollowerID = userId;
            await _followerService.DeleteAsync(dto, cancellationToken);
            return NoContent();
        }

        [Authorize]
        [HttpGet("is-following")]
        public async Task<IActionResult> IsFollowing([FromQuery] int followingId)
        {
            var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdFromToken == null || !int.TryParse(userIdFromToken, out var followerId))
                return Unauthorized("Invalid token.");

            var result = await _followerService.IsFollowingAsync(new FollowerDto
            {
                FollowerID = followerId,
                FollowingID = followingId
            });

            return Ok(result);
        }

        [Authorize]
        [HttpGet("{userId}/following")]
        public async Task<IActionResult> GetFollowing(int userId)
        {
            var users = await _followerService.GetFollowingUsersAsync(userId);
            return Ok(users);
        }

        [Authorize]
        [HttpGet("{userId}/followers")]
        public async Task<IActionResult> GetFollowers(int userId)
        {
            var users = await _followerService.GetFollowerUsersAsync(userId);
            return Ok(users);
        }
        
        [HttpGet("{userId}/following-count")]
        public async Task<IActionResult> GetFollowingCount(int userId)
        {
            var count = await _followerService.GetFollowingCountAsync(userId);
            return Ok(count);
        }
        
        [HttpGet("{userId}/followers-count")]
        public async Task<IActionResult> GetFollowerCount(int userId)
        {
            var count = await _followerService.GetFollowerCountAsync(userId);
            return Ok(count);
        }
    }
}