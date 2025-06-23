using DAL.Context;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class SavedPostRepository : GenericRepository<SavedPost>, ISavedPostRepository
{
    public SavedPostRepository(WardrobeDbContext context) : base(context)
    {
    }

    public async Task<bool> IsPostSavedAsync(int userId, int publicationId)
    {
        return await context.SavedPosts.AnyAsync(sp => sp.UserID == userId && sp.PublicationID == publicationId);
    }
    
    public async Task<SavedPost?> GetByUserAndPublicationAsync(int userId, int publicationId)
    {
        return await context.SavedPosts
            .FirstOrDefaultAsync(sp => sp.UserID == userId && sp.PublicationID == publicationId);
    }
}