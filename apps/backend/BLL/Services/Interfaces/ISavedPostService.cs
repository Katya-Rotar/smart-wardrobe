using BLL.DTO.SavedPost;

namespace BLL.Services.Interfaces;

public interface ISavedPostService
{
    Task<int> SavePostAsync(SavedPostDto dto, CancellationToken cancellationToken);
    Task RemoveSavedPostAsync(SavedPostDto dto, CancellationToken cancellationToken);
    Task<bool> IsPostSavedAsync(SavedPostDto dto);
}