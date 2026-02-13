using DAL.Entities;

namespace DAL.Repositories.Interfaces;

public interface ICommentRepository : IGenericRepository<Comment>
{
    Task<List<Comment>> GetCommentsByPublicationIdAsync(int publicationId);
}
