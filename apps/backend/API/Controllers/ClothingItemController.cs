using System.Security.Claims;
using BLL.DTO;
using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Helpers;
using DAL.Helpers.Params;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClothingItemController : ControllerBase
    {
        private readonly IClothingItemService _clothingItemService;

        public ClothingItemController(IClothingItemService clothingItemService)
        {
            _clothingItemService = clothingItemService;
        }

        // GET: api/clothingitem
        [Authorize]
        [HttpGet]
        public ActionResult<PagedList<ClothingItemAllDto>> GetAllClothingItems([FromQuery] ClothingItemParams parameters)
        {
            var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdFromToken == null || !int.TryParse(userIdFromToken, out var userId))
                return Unauthorized("You are not authorized to access clothing items.");
            parameters.UserId = userId;
            var clothingItems = _clothingItemService.GetAllClothingItems(parameters);
            return Ok(clothingItems);
        }


        // GET: api/clothingitem/type/{typeId}
        [Authorize]
        [HttpGet("type/{typeId}")]
        public async Task<ActionResult> GetItemsByTypeAsync(int typeId, [FromQuery] ClothingItemParams parameters)
        {
            var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdFromToken != null && int.TryParse(userIdFromToken, out var userId))
            {
                var clothingItems = await _clothingItemService.GetItemsByTypeAsync(userId, typeId, parameters);
                return Ok(clothingItems);
            }

            return Unauthorized("You are not authorized.");
        }

        // GET: api/clothingitem/{id}
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ClothingItemDto>> GetClothingItemById(int id)
        {
            var clothingItem = await _clothingItemService.GetClothingItemByIdAsync(id);
            return Ok(clothingItem);
        }

        // GET: api/clothingitem/details/{id}
        [Authorize]
        [HttpGet("details/{id}")]
        public async Task<ActionResult<ClothingItem>> GetClothingItemDetailsAsync(int id)
        {
            var clothingItem = await _clothingItemService.GetClothingItemDetailsAsync(id);
            return Ok(clothingItem);
        }

        // GET: api/clothingitem/grouped/{userId}
        [Authorize]
        [HttpGet("grouped")]
        public async Task<ActionResult> GetClothingItemsGroupedByTypeAsync()
        {
            var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdFromToken == null || !int.TryParse(userIdFromToken, out var userId))
                return Unauthorized("Invalid token or user ID.");

            var result = await _clothingItemService.GetClothingItemsGroupedByTypeAsync(userId);
            return Ok(result);
        }

        // POST: api/clothingitem
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddClothingItemAsync(
            [FromBody] CreateClothingItemDto clothingItemDto,
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdFromToken == null || !int.TryParse(userIdFromToken, out var userId))
                return Unauthorized("Invalid token or user ID.");

            clothingItemDto.UserID = userId;

            var itemId = await _clothingItemService.AddClothingItemAsync(clothingItemDto, cancellationToken);

            return CreatedAtAction(nameof(GetClothingItemById), new { id = itemId }, clothingItemDto);
        }

        // PUT: api/clothingitem/{id}
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateClothingItemAsync(int id, [FromBody] ClothingItemDto clothingItemDto, 
            CancellationToken cancellationToken)
        {
            await _clothingItemService.UpdateClothingItemAsync(clothingItemDto, cancellationToken);
            return NoContent();
        }

        // DELETE: api/clothingitem/{id}
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteClothingItemAsync(int id, CancellationToken cancellationToken)
        {
            await _clothingItemService.DeleteClothingItemAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
