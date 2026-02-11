using BLL.DTO.Tag;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagDto>>> GetAll()
        {
            var tag = await _tagService.GetAll();
            return Ok(tag);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TagDto>> GetTagById(int id)
        {
            var tag = await _tagService.GetTagByIdAsync(id);
            return Ok(tag);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddTagAsync([FromBody] CreateTagDto createTagDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var tagId = await _tagService.AddTagAsync(createTagDto, cancellationToken);
            return CreatedAtAction(nameof(GetTagById), new { id = tagId }, createTagDto);
        }
    }
}