using DAL.Entities;

namespace DAL.Repositories.Interfaces;

public interface IPublicationTagRepository : IGenericRepository<PublicationTag>
{
    Task<IEnumerable<PublicationTag>> GetTagsByPublicationIdAsync(int publicationId);
    Task DeleteByPublicationAndTagAsync(int publicationId, int tagId);
}