using BLL.DTO.Type;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        private readonly ITypeService _typeService;

        public TypeController(ITypeService typeService)
        {
            _typeService = typeService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetAllTypes()
        {
            var type = await _typeService.GetAll();
            return Ok(type);
        }
        
        [HttpGet("by-category/{categoryId}")]
        public async Task<IActionResult> GetTypesByCategory(int categoryId)
        {
            var types = await _typeService.GetTypesByCategoryIdAsync(categoryId);
            return Ok(types);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TypeDto>> GetTypeById(int id)
        {
            var tag = await _typeService.GetTypeAsync(id);
            return Ok(tag);
        }
        
    }
}