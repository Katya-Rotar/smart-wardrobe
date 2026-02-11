using BLL.DTO.OutfitGroup;

namespace BLL.Services.Interfaces;

public interface IOutfitGroupService
{
    Task<IEnumerable<OutfitGroupDto>> GetByUserIdAsync(int userId);
    Task<OutfitGroupDto?> GetByIdAsync(int id);
    Task CreateAsync(CreateOutfitGroupDto dto, CancellationToken cancellationToken);
    Task UpdateAsync(OutfitGroupDto dto, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
}