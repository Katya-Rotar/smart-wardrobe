using BLL.DTO.Season;

namespace BLL.Services.Interfaces;

public interface ISeasonService
{
    Task<IEnumerable<SeasonDto>> GetAll();
}