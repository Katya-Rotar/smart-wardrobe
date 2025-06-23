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
            var id = await _followerService.AddFollowerAsync(dto, cancellationToken);
            return Ok(new { id });
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Unfollow([FromBody] FollowerDto dto, CancellationToken cancellationToken)
        {
            await _followerService.DeleteAsync(dto, cancellationToken);
            return NoContent();
        }

        [Authorize]
        [HttpGet("is-following")]
        public async Task<IActionResult> IsFollowing([FromQuery] int followerId, [FromQuery] int followingId)
        {
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
        
        [Authorize]
        [HttpGet("{userId}/following-count")]
        public async Task<IActionResult> GetFollowingCount(int userId)
        {
            var count = await _followerService.GetFollowingCountAsync(userId);
            return Ok(count);
        }

        [Authorize]
        [HttpGet("{userId}/followers-count")]
        public async Task<IActionResult> GetFollowerCount(int userId)
        {
            var count = await _followerService.GetFollowerCountAsync(userId);
            return Ok(count);
        }
    }
}