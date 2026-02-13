using System.Security.Claims;
using BLL.DTO.Comment;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] CreateCommentDto dto,
            CancellationToken cancellationToken)
        {
            var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdFromToken == null || !int.TryParse(userIdFromToken, out var userId))
                return Unauthorized("Invalid token.");
            
            dto.UserID = userId;

            var id = await _commentService.AddCommentAsync(dto, cancellationToken);
            return Ok(new { id });
        }

        [HttpGet("publication/{publicationId}")]
        public async Task<IActionResult> GetCommentsByPublication(int publicationId)
        {
            var comments = await _commentService.GetCommentsByPublicationAsync(publicationId);
            return Ok(comments);
        }
    }
}