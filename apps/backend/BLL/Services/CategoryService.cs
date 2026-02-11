using BLL.DTO.Category;
using BLL.Services.Interfaces;
using AutoMapper;
using DAL.Repositories.Interfaces;

namespace BLL.Services;

public class CategoryService: ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryDto>> GetAll()
    {
        var category = await _unitOfWork.Categories.GetAllAsync();
        return _mapper.Map<IEnumerable<CategoryDto>>(category);
    }
}