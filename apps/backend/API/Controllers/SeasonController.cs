using BLL.DTO.Season;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SeasonController : ControllerBase
    {
        private readonly ISeasonService _seasonService;

        public SeasonController(ISeasonService seasonService)
        {
            _seasonService = seasonService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SeasonDto>>> GetAllSeasons()
        {
            var seasons = await _seasonService.GetAll();
            return Ok(seasons);
        }
    }
}