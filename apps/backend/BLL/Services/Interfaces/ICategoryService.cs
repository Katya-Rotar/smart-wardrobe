using BLL.DTO.Category;
using DAL.Entities;

namespace BLL.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAll();
}