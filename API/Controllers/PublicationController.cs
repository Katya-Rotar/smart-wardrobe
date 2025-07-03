using System.Security.Claims;
using BLL.DTO.Publication;
using BLL.Services.Interfaces;
using DAL.Helpers;
using DAL.Helpers.Params;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublicationController : ControllerBase
    {
        private readonly IPublicationService _publicationService;

        public PublicationController(IPublicationService publicationService)
        {
            _publicationService = publicationService;
        }
        
        [HttpGet]
        public ActionResult<PagedList<PublicationListDto>> GetAll([FromQuery] PublicationParams parameters)
        {
            var publications = _publicationService.GetAllPublication(parameters);
            return Ok(publications);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<PublicationDetailsDto>> GetById(int id)
        {
            var publication = await _publicationService.GetPublicationDetailsAsync(id);
            if (publication == null)
                return NotFound();

            return Ok(publication);
        }
        
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<PagedList<PublicationListDto>>> GetByUser(int userId, [FromQuery] PublicationParams parameters)
        {
            var publications = await _publicationService.GetByUserAsync(userId, parameters);
            return Ok(publications);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] PublicationDto dto, CancellationToken cancellationToken)
        {
            var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdFromToken == null || !int.TryParse(userIdFromToken, out var userId))
                return Unauthorized("Invalid token or user ID.");

            dto.UserID = userId;

            var id = await _publicationService.CreatePublicationAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePublicationDto dto, CancellationToken cancellationToken)
        {
            if (id != dto.Id)
                return BadRequest("Mismatched ID");

            var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdFromToken == null || !int.TryParse(userIdFromToken, out var userId))
                return Unauthorized("Invalid token.");

            var publication = await _publicationService.GetPublicationDetailsAsync(id);
            if (publication == null)
                return NotFound("Publication not found.");

            if (publication.UserID != userId)
                return Forbid("You are not allowed to update this publication.");

            await _publicationService.UpdatePublicationAsync(id, dto, cancellationToken);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdFromToken == null || !int.TryParse(userIdFromToken, out var userId))
                return Unauthorized("Invalid token.");

            var publication = await _publicationService.GetPublicationDetailsAsync(id);
            if (publication == null)
                return NotFound("Publication not found.");

            if (publication.UserID != userId)
                return Forbid("You are not allowed to delete this publication.");

            await _publicationService.DeletePublicationAsync(id, cancellationToken);
            return NoContent();
        }
        
        [Authorize]
        [HttpGet("followings")]
        public async Task<ActionResult<PagedList<PublicationListDto>>> GetFollowingsPublications([FromQuery] PublicationParams parameters)
        {
            var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdFromToken == null || !int.TryParse(userIdFromToken, out var userId))
                return Unauthorized("Invalid token.");

            var publications = await _publicationService.GetFollowingsPublicationsAsync(userId, parameters);
            return Ok(publications);
        }
        
        [Authorize]
        [HttpGet("saved")]
        public async Task<ActionResult<PagedList<PublicationListDto>>> GetSavedPublications([FromQuery] PublicationParams parameters)
        {
            var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdFromToken == null || !int.TryParse(userIdFromToken, out var userId))
                return Unauthorized("Invalid token.");

            var publications = await _publicationService.GetSavedPublicationsAsync(userId, parameters);
            return Ok(publications);
        }
    }
}
