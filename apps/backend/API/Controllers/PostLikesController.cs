using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BLL.DTO.PostLike;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostLikesController : ControllerBase
{
    private readonly IPostLikeService _postLikeService;

    public PostLikesController(IPostLikeService postLikeService)
    {
        _postLikeService = postLikeService;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Like([FromBody] PostLikeDto dto, CancellationToken cancellationToken)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        dto.UserID = userId;

        var id = await _postLikeService.AddLikeAsync(dto, cancellationToken);
        return Ok(new { id });
    }

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> Unlike([FromBody] PostLikeDto dto, CancellationToken cancellationToken)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        dto.UserID = userId;

        await _postLikeService.RemoveLikeAsync(dto, cancellationToken);
        return NoContent();
    }

    [Authorize]
    [HttpGet("is-liked")]
    public async Task<IActionResult> IsLiked([FromQuery] int publicationId)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var dto = new PostLikeDto { UserID = userId, PublicationID = publicationId };

        var result = await _postLikeService.IsPostLikedAsync(dto);
        return Ok(result);
    }

    [HttpGet("{publicationId}/count")]
    public async Task<IActionResult> GetLikesCount(int publicationId)
    {
        var count = await _postLikeService.GetLikesCountAsync(publicationId);
        return Ok(count);
    }
}
