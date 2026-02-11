using System.Security.Claims;
using BLL.DTO.OutfitGroup;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OutfitGroupController : ControllerBase
    {
        private readonly IOutfitGroupService _outfitGroupService;

        public OutfitGroupController(IOutfitGroupService outfitGroupService)
        {
            _outfitGroupService = outfitGroupService;
        }

        [Authorize]
        [HttpGet("group")]
        public async Task<ActionResult<IEnumerable<OutfitGroupDto>>> GetGroupsByUserId()
        {
            var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdFromToken == null || !int.TryParse(userIdFromToken, out var userId))
                return Unauthorized("Invalid or missing token.");

            var groups = await _outfitGroupService.GetByUserIdAsync(userId);
            return Ok(groups);
        }


        // GET: api/outfitgroup/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<OutfitGroupDto>> GetGroupById(int id)
        {
            var group = await _outfitGroupService.GetByIdAsync(id);
            if (group == null) return NotFound();
            return Ok(group);
        }

        // POST: api/outfitgroup
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateGroup([FromBody] CreateOutfitGroupDto dto, CancellationToken cancellationToken)
        {
            var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdFromToken == null || !int.TryParse(userIdFromToken, out var userId))
                return Unauthorized("Invalid or missing token.");

            dto.UserID = userId;

            await _outfitGroupService.CreateAsync(dto, cancellationToken);
            return StatusCode(201);
        }

        // PUT: api/outfitgroup
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateGroup([FromBody] OutfitGroupDto dto, CancellationToken cancellationToken)
        {
            await _outfitGroupService.UpdateAsync(dto, cancellationToken);
            return NoContent();
        }

        // DELETE: api/outfitgroup/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(int id, CancellationToken cancellationToken)
        {
            await _outfitGroupService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
