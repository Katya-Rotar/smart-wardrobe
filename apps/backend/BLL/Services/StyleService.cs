using AutoMapper;
using BLL.DTO.Style;
using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interfaces;

namespace BLL.Services;

public class StyleService : IStyleService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public StyleService (IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<StyleDto>> GetAll()
    {
        var style = await _unitOfWork.Styles.GetAllAsync();
        return _mapper.Map<IEnumerable<StyleDto>>(style);
    }
}