using BLL.DTO.Tag;

namespace BLL.Services.Interfaces;

public interface ITagService
{
    Task<IEnumerable<TagDto>> GetAll();
    Task<TagDto> GetTagByIdAsync(int id);
    Task<int> AddTagAsync(CreateTagDto tagDto, CancellationToken cancellationToken);
}