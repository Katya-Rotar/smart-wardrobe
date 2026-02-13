using DAL.Entities;

namespace DAL.Repositories.Interfaces;

public interface ISavedPostRepository : IGenericRepository<SavedPost>
{
    Task<bool> IsPostSavedAsync(int userId, int publicationId);
    Task<SavedPost?> GetByUserAndPublicationAsync(int userId, int publicationId);
}