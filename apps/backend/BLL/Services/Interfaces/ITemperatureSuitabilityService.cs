using BLL.DTO.TemperatureSuitability;

namespace BLL.Services.Interfaces;

public interface ITemperatureSuitabilityService
{
    Task<IEnumerable<TemperatureSuitabilityDto>> GetAll();
}