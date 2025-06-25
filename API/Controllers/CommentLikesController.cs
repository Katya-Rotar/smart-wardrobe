using System.Security.Claims;
using BLL.DTO.CommentLike;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentLikesController : ControllerBase
    {
        private readonly ICommentLikeService _commentLikeService;

        public CommentLikesController(ICommentLikeService commentLikeService)
        {
            _commentLikeService = commentLikeService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Like([FromBody] CommentLikeDto dto, CancellationToken cancellationToken)
        {
            dto.UserID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var id = await _commentLikeService.AddLikeAsync(dto, cancellationToken);
            return Ok(new { id });
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Unlike([FromBody] CommentLikeDto dto, CancellationToken cancellationToken)
        {
            dto.UserID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            await _commentLikeService.RemoveLikeAsync(dto, cancellationToken);
            return NoContent();
        }

        [Authorize]
        [HttpGet("is-liked")]
        public async Task<IActionResult> IsLiked([FromQuery] int commentId)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var dto = new CommentLikeDto { UserID = userId, CommentID = commentId };
            var result = await _commentLikeService.IsCommentLikedAsync(dto);
            return Ok(result);
        }

        [HttpGet("{commentId}/count")]
        public async Task<IActionResult> GetLikesCount(int commentId)
        {
            var count = await _commentLikeService.GetLikesCountAsync(commentId);
            return Ok(count);
        }
    }
}
