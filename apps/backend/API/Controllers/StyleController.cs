using BLL.DTO.Style;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StyleController : ControllerBase
    {
        private readonly IStyleService _styleService;

        public StyleController(IStyleService styleService)
        {
            _styleService = styleService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StyleDto>>> GetAllStyles()
        {
            var style = await _styleService.GetAll();
            return Ok(style);
        }
    }
}