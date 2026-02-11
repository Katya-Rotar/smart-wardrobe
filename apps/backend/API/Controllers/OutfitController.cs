using System.Security.Claims;
using BLL.DTO.Outfit;
using BLL.Services.Interfaces;
using DAL.Helpers;
using DAL.Helpers.Params;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OutfitController : ControllerBase
    {
        private readonly IOutfitService _outfitService;

        public OutfitController(IOutfitService outfitService)
        {
            _outfitService = outfitService;
        }

        // GET: api/Outfit
        [Authorize]
        [HttpGet]
        public ActionResult<PagedList<OutfitDto>> GetAllOutfits([FromQuery] OutfitParams parameters)
        {
            var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdFromToken == null || !int.TryParse(userIdFromToken, out var userId))
                return Unauthorized("Invalid token or user ID.");

            parameters.UserId = userId;

            var outfits = _outfitService.GetAllOutfit(parameters);
            return Ok(outfits);
        }

        // GET: api/Outfit/{id}
        [Authorize]
        [HttpGet("detail/{id}")]
        public async Task<ActionResult<OutfitDto>> GetOutfitDetails(int id)
        {
            var outfit = await _outfitService.GetOutfitDetailsAsync(id);
            if (outfit == null)
                return NotFound();

            return Ok(outfit);
        }
        
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<UpdateOutfitDto>> GetOutfit(int id)
        {
            var outfit = await _outfitService.GetOutfitAsync(id);
            if (outfit == null)
                return NotFound();

            return Ok(outfit);
        }

        // GET: api/Outfit/by-item/{itemId}
        [Authorize]
        [HttpGet("by-item/{itemId}")]
        public async Task<ActionResult<IEnumerable<OutfitDto>>> GetOutfitsByItemId(int itemId)
        {
            var outfits = await _outfitService.GetOutfitsByItemIdAsync(itemId);
            return Ok(outfits);
        }
        
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddOutfitAsync([FromBody] CreateOutfitDto createOutfitDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdFromToken == null || !int.TryParse(userIdFromToken, out var userId))
                return Unauthorized("Invalid token or user ID.");

            createOutfitDto.UserID = userId;

            var outfitId = await _outfitService.CreateOutfitAsync(createOutfitDto, cancellationToken);
            return CreatedAtAction(nameof(GetOutfitDetails), new { id = outfitId }, createOutfitDto);
        }
        
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOutfitAsync(int id, [FromBody] UpdateOutfitDto outfitDto, 
            CancellationToken cancellationToken)
        {
            await _outfitService.UpdateOutfitAsync(id, outfitDto, cancellationToken);
            return NoContent();
        }
        
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOutfitAsync(int id, CancellationToken cancellationToken)
        {
            await _outfitService.DeleteOutfitAsync(id, cancellationToken);
            return NoContent();
        }
    }
}