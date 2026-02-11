using BLL.DTO.Type;

namespace BLL.Services.Interfaces;

public interface ITypeService
{
    Task<TypeDto> GetTypeAsync(int id);
    Task<IEnumerable<TypeDto>> GetAll();
    Task<IEnumerable<TypeDto>> GetTypesByCategoryIdAsync(int categoryId);
}