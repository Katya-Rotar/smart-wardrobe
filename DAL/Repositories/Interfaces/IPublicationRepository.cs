using DAL.Entities;
using DAL.Helpers;
using DAL.Helpers.Params;

namespace DAL.Repositories.Interfaces;

public interface IPublicationRepository : IGenericRepository<Publication>
{
    PagedList<Publication> GetAllPublication(PublicationParams parameters);
    Task<Publication?> GetPublicationDetailsAsync(int id);
    Task<PagedList<Publication>> GetByUserAsync(int userId, PublicationParams parameters);
}