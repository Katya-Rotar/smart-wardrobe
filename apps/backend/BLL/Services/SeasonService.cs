using AutoMapper;
using BLL.DTO.Season;
using BLL.Services.Interfaces;
using DAL.Repositories.Interfaces;

namespace BLL.Services;

public class SeasonService : ISeasonService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SeasonService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<SeasonDto>> GetAll()
    {
        var season = await _unitOfWork.Seasons.GetAllAsync();
        return _mapper.Map<IEnumerable<SeasonDto>>(season);
    }
}