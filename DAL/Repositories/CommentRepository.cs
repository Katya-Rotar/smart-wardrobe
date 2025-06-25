using DAL.Context;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class CommentRepository : GenericRepository<Comment>, ICommentRepository
{
    public CommentRepository(WardrobeDbContext context) : base(context) { }

    public async Task<List<Comment>> GetCommentsByPublicationIdAsync(int publicationId)
    {
        return await context.Comments
            .Where(c => c.PublicationID == publicationId)
            .Include(c => c.User)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
    }
}