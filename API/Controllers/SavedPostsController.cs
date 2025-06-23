using BLL.DTO.SavedPost;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SavedPostsController : ControllerBase
    {
        private readonly ISavedPostService _savedPostService;

        public SavedPostsController(ISavedPostService savedPostService)
        {
            _savedPostService = savedPostService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SavePost([FromBody] SavedPostDto dto, CancellationToken cancellationToken)
        {
            var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdFromToken == null || !int.TryParse(userIdFromToken, out var userId))
                return Unauthorized("Invalid token.");

            dto.UserID = userId;

            var id = await _savedPostService.SavePostAsync(dto, cancellationToken);
            return Ok(new { id });
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> RemoveSavedPost([FromBody] SavedPostDto dto, CancellationToken cancellationToken)
        {
            var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdFromToken == null || !int.TryParse(userIdFromToken, out var userId))
                return Unauthorized("Invalid token.");

            dto.UserID = userId;

            await _savedPostService.RemoveSavedPostAsync(dto, cancellationToken);
            return NoContent();
        }

        [Authorize]
        [HttpGet("is-saved")]
        public async Task<IActionResult> IsPostSaved([FromQuery] int publicationId)
        {
            var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdFromToken == null || !int.TryParse(userIdFromToken, out var userId))
                return Unauthorized("Invalid token.");

            var dto = new SavedPostDto
            {
                UserID = userId,
                PublicationID = publicationId
            };

            var isSaved = await _savedPostService.IsPostSavedAsync(dto);
            return Ok(isSaved);
        }
    }
}
