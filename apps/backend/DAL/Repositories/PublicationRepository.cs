using DAL.Context;
using DAL.Entities;
using DAL.Helpers;
using DAL.Helpers.Params;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class PublicationRepository : GenericRepository<Publication>, IPublicationRepository
{
    public PublicationRepository(WardrobeDbContext context) : base(context)
    {
    }

    public PagedList<Publication> GetAllPublication(PublicationParams parameters)
    {
        var query = context.Publications
            .Include(p => p.PublicationTags).ThenInclude(pt => pt.Tag)
            .Include(p => p.User)
            .Include(p => p.Outfit)
            .ThenInclude(o => o.Items).ThenInclude(oi => oi.ClothingItem)
            .ThenInclude(ci => ci.Category)
            .Include(p => p.Outfit)
            .ThenInclude(o => o.Items).ThenInclude(oi => oi.ClothingItem)
            .ThenInclude(ci => ci.Type)
            .Include(p => p.Outfit.Styles)
            .Include(p => p.Outfit.Seasons)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(parameters.SearchQuery))
        {
            var search = parameters.SearchQuery.Trim().ToLower();

            query = query.Where(p =>
                p.PublicationTags.Any(pt => pt.Tag.TagName.ToLower().Contains(search)) ||
                
                p.User.Username.ToLower().Contains(search) ||
                
                p.Outfit.Items.Any(oi =>
                    oi.ClothingItem.Name.ToLower().Contains(search) ||
                    oi.ClothingItem.Color.ToLower().Contains(search) ||
                    
                    (oi.ClothingItem.Category != null && oi.ClothingItem.Category.CategoryName.ToLower().Contains(search)) ||
                    (oi.ClothingItem.Type != null && oi.ClothingItem.Type.TypeName.ToLower().Contains(search))
                ) ||
                
                p.Outfit.Styles.Any(style =>
                    style.Style.StyleName.ToLower().Contains(search)
                ) ||
                
                p.Outfit.Seasons.Any(season =>
                    season.Season.SeasonName.ToLower().Contains(search)
                )
            );
        }

        return PagedList<Publication>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
    }

    public async Task<Publication?> GetPublicationDetailsAsync(int id)
    {
        var query =  context.Publications
            .Include(p => p.User)
            .Include(p => p.Outfit)
            .ThenInclude(o => o.Items)
            .ThenInclude(oi => oi.ClothingItem)
            .Include(p => p.PublicationTags)
            .ThenInclude(pt => pt.Tag)
            .FirstOrDefaultAsync(p => p.Id == id);
        return await query;
    }

    public Task<PagedList<Publication>> GetByUserAsync(int userId, PublicationParams parameters)
    {
        var query = context.Publications
            .Where(p => p.UserID == userId)
            .Include(p => p.PublicationTags)
            .ThenInclude(pt => pt.Tag)
            .Include(p => p.User)
            .Include(p => p.Outfit)
            .OrderByDescending(p => p.Id);
        return Task.FromResult(PagedList<Publication>.ToPagedList(query, parameters.PageNumber, parameters.PageSize));

    }
    
    public async Task<PagedList<Publication>> GetPublicationsOfFollowingsAsync(int userId, PublicationParams parameters)
    {
        var followingIds = await context.Followers
            .Where(f => f.FollowerID == userId)
            .Select(f => f.FollowingID)
            .ToListAsync();

        var query = context.Publications
            .Where(p => followingIds.Contains(p.UserID))
            .Include(p => p.PublicationTags).ThenInclude(pt => pt.Tag)
            .Include(p => p.User)
            .Include(p => p.Outfit).ThenInclude(o => o.Items)
            .AsQueryable();

        return PagedList<Publication>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
    }
    
    public async Task<PagedList<Publication>> GetSavedPublicationsAsync(int userId, PublicationParams parameters)
    {
        var savedPublicationIds = await context.SavedPosts
            .Where(sp => sp.UserID == userId)
            .Select(sp => sp.PublicationID)
            .ToListAsync();

        var query = context.Publications
            .Where(p => savedPublicationIds.Contains(p.Id))
            .Include(p => p.PublicationTags).ThenInclude(pt => pt.Tag)
            .Include(p => p.User)
            .Include(p => p.Outfit).ThenInclude(o => o.Items)
            .AsQueryable();

        return PagedList<Publication>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
    }
}