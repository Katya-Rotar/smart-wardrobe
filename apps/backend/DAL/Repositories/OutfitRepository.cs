using DAL.Context;
using DAL.Entities;
using DAL.Helpers;
using DAL.Helpers.Filtering;
using DAL.Helpers.Params;
using DAL.Helpers.Search;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class OutfitRepository : GenericRepository<Outfit>, IOutfitRepository
{
    private readonly ISearchHelper<Outfit> _searchHelper;
    
    public OutfitRepository(WardrobeDbContext context, ISearchHelper<Outfit> searchHelper) : base(context)
    {
        _searchHelper = searchHelper;
    }

    public PagedList<Outfit> GetAllOutfit(OutfitParams parameters)
    {
        var query = context.Outfits
            .Include(o => o.TemperatureSuitability)
            .Include(o => o.Styles).ThenInclude(s => s.Style)
            .Include(o => o.Seasons).ThenInclude(s => s.Season)
            .Include(o => o.Items).ThenInclude(s => s.ClothingItem)
            .Include(o => o.GroupItems).ThenInclude(g => g.OutfitGroup)
            .Include(o => o.Tags).ThenInclude(t => t.Tag)
            .Where(o => o.UserID == parameters.UserId)
            .AsQueryable();
        
        ApplyFilters(ref query, parameters);
        
        if (!string.IsNullOrWhiteSpace(parameters.SearchQuery))
        {
            string search = parameters.SearchQuery.Trim().ToLower();

            query = query.Where(o => o.Tags.Any(t => t.Tag != null && 
                                                     t.Tag.TagName.ToLower().Contains(search)));
        }

        
        return PagedList<Outfit>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
    }

    public async Task<Outfit?> GetOutfitDetailsAsync(int id)
    {
        var query = context.Outfits
            .Include(o => o.TemperatureSuitability)
            .Include(o => o.Styles).ThenInclude(s => s.Style)
            .Include(o => o.Seasons).ThenInclude(s => s.Season)
            .Include(o => o.Items).ThenInclude(s => s.ClothingItem)
            .Include(o => o.GroupItems).ThenInclude(g => g.OutfitGroup)
            .Include(o => o.Tags).ThenInclude(t => t.Tag)
            .FirstOrDefaultAsync(c => c.Id == id);
        
        return await query;
    }

    public override async Task<Outfit> GetByIdAsync(int id)
    {
        var outfit = await context.Outfits
            .Include(o => o.Styles)
            .Include(o => o.Seasons)
            .Include(o => o.Tags)
            .Include(o => o.GroupItems)
            .Include(o => o.TemperatureSuitability)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (outfit == null)
            throw new KeyNotFoundException($"Outfit with ID {id} not found.");

        return outfit;
    }

    public async Task<IEnumerable<Outfit>> GetOutfitsByItemIdAsync(int itemId)
    {
        return await context.Outfits
            .Include(o => o.TemperatureSuitability)
            .Include(o => o.Styles).ThenInclude(s => s.Style)
            .Include(o => o.Seasons).ThenInclude(s => s.Season)
            .Include(o => o.Items).ThenInclude(i => i.ClothingItem)
            .Include(o => o.GroupItems).ThenInclude(g => g.OutfitGroup)
            .Include(o => o.Tags).ThenInclude(t => t.Tag)
            .Where(o => o.Items.Any(i => i.ClothingItemID == itemId))
            .ToListAsync();
    }
    
    private static void ApplyFilters(ref IQueryable<Outfit> query, OutfitParams parameters)
    {
        var predicate = PredicateBuilder.True<Outfit>();

        if (!string.IsNullOrWhiteSpace(parameters.TemperatureSuitabilityName))
        {
            var temp = parameters.TemperatureSuitabilityName.ToLower();
            predicate = predicate.And(c => c.TemperatureSuitability != null && c.TemperatureSuitability.TemperatureSuitabilityName.ToLower() == temp);
        }

        if (!string.IsNullOrWhiteSpace(parameters.StyleName))
        {
            var style = parameters.StyleName.ToLower();
            predicate = predicate.And(c => c.Styles.Any(s => s.Style.StyleName.ToLower() == style));
        }

        if (!string.IsNullOrWhiteSpace(parameters.SeasonName))
        {
            var season = parameters.SeasonName.ToLower();
            predicate = predicate.And(c => c.Seasons.Any(s => s.Season.SeasonName.ToLower() == season));
        }
        
        if (!string.IsNullOrWhiteSpace(parameters.GroupName))
        {
            var group = parameters.GroupName.ToLower();
            predicate = predicate.And(c => c.GroupItems.Any(s => s.OutfitGroup.GroupName.ToLower() == group));
        }

        query = query.Where(predicate);
    }
}