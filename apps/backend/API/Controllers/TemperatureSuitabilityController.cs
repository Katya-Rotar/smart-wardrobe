using BLL.DTO.TemperatureSuitability;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemperatureSuitabilityController : ControllerBase
    {
        private readonly ITemperatureSuitabilityService _temperatureSuitabilityService;

        public TemperatureSuitabilityController(ITemperatureSuitabilityService temperatureSuitabilityService)
        {
            _temperatureSuitabilityService = temperatureSuitabilityService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TemperatureSuitabilityDto>>> GetAllTemperatureSuitability()
        {
            var temp = await _temperatureSuitabilityService.GetAll();
            return Ok(temp);
        }
    }
}