using AutoMapper;
using BLL.DTO.TemperatureSuitability;
using BLL.Services.Interfaces;
using DAL.Repositories.Interfaces;

namespace BLL.Services;

public class TemperatureSuitabilityService : ITemperatureSuitabilityService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TemperatureSuitabilityService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TemperatureSuitabilityDto>> GetAll()
    {
        var temp = await _unitOfWork.TemperatureSuitability.GetAllAsync();
        return _mapper.Map<IEnumerable<TemperatureSuitabilityDto>>(temp);
    }
}