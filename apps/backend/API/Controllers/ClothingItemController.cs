using System.Security.Claims;
using BLL.DTO;
using BLL.Services.Interfaces;
using BLL.Services;
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
        private readonly MlServiceClient _mlService;
        private readonly IRecommendationService _recommendationService;

        public ClothingItemController(IClothingItemService clothingItemService, MlServiceClient mlService, IRecommendationService recommendationService)
        {
            _clothingItemService = clothingItemService;
            _mlService = mlService;
            _recommendationService = recommendationService;
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
        public async Task<ActionResult<ClothingItemAllDto>> GetClothingItemDetailsAsync(int id)
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
            if (id != clothingItemDto.Id) 
                return BadRequest("ID mismatch");

            var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdFromToken == null || !int.TryParse(userIdFromToken, out var userId))
                return Unauthorized();
            
            clothingItemDto.UserID = userId; 

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
        
        [Authorize]
        [HttpPost("predict")]
        public async Task<IActionResult> Predict(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Файл не завантажено");

            try 
            {
                using var stream = file.OpenReadStream();
                var prediction = await _mlService.PredictImageAsync(stream, file.FileName, file.ContentType);

                return Ok(prediction);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "ML Service error: " + ex.Message);
            }
        }
        
        [Authorize]
        [HttpGet("generate")]
        public async Task<IActionResult> GetRecommendations([FromQuery] float temp, [FromQuery] int weatherCode, [FromQuery] int styleId)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized();
            
            if (!int.TryParse(userIdClaim.Value, out int userId)) 
            {
                return BadRequest("Invalid User ID in token.");
            }
            
            var result = await _recommendationService.GetRecommendedItemsAsync(userId, temp, weatherCode, styleId);
            return Ok(result);
        }
    }
}
