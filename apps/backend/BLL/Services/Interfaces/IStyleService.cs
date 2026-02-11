using BLL.DTO.Style;

namespace BLL.Services.Interfaces;

public interface IStyleService
{
    Task<IEnumerable<StyleDto>> GetAll();
}