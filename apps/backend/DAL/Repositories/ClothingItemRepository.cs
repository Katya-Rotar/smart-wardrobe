using DAL.Context;
using DAL.Entities;
using DAL.Helpers;
using DAL.Helpers.Filtering;
using DAL.Helpers.Params;
using DAL.Helpers.Search;
using DAL.Helpers.Sorting;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class ClothingItemRepository : GenericRepository<ClothingItem>, IClothingItemRepository
{
    private readonly ISortHelper<ClothingItem> _sortHelper;
    private readonly ISearchHelper<ClothingItem> _searchHelper;
    
    public ClothingItemRepository(WardrobeDbContext context, ISortHelper<ClothingItem> sortHelper,
        ISearchHelper<ClothingItem> searchHelper) : base(context)
    {
        _sortHelper = sortHelper;
        _searchHelper = searchHelper;
    }
    
    public PagedList<ClothingItem> GetAllClothingItems(ClothingItemParams parameters, IEnumerable<string> searchFields)
    {
        var query = context.ClothingItems
            .Include(c => c.Type)
            .Include(c => c.Category)
            .Include(c => c.TemperatureSuitability)
            .Include(c => c.Styles).ThenInclude(cs => cs.Style)
            .Include(c => c.Seasons).ThenInclude(cs => cs.Season)
            .Where(c => c.UserID == parameters.UserId)
            .AsQueryable();
        
        ApplyFilters(ref query, parameters);
        
        if (!string.IsNullOrWhiteSpace(parameters.SearchQuery))
        {
            query = _searchHelper.ApplySearch(query, parameters.SearchQuery, searchFields);
        }
        
        query = _sortHelper.ApplySort(query, parameters.OrderBy);
        
        return PagedList<ClothingItem>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
    }
    
    public async Task<Dictionary<string, List<ClothingItem>>> GetClothingItemsGroupedByTypeAsync(int userId)
    {
        return await context.ClothingItems
            .Include(c => c.Type)
            .Where(c => c.UserID == userId)
            .GroupBy(c => c.Type!.TypeName)
            .ToDictionaryAsync(
                g => g.Key,
                g => g.ToList()
            );
    }
    
    public Task<PagedList<ClothingItem>> GetByTypeAsync(int userId, int typeId, ClothingItemParams parameters)
    {
        var query = context.ClothingItems
            .Include(c => c.Type)
            .Where(item => item.TypeID == typeId && item.UserID == userId);

        return Task.FromResult(PagedList<ClothingItem>.ToPagedList(query, parameters.PageNumber, parameters.PageSize));
    }


    private static void ApplyFilters(ref IQueryable<ClothingItem> query, ClothingItemParams parameters)
    {
        var predicate = PredicateBuilder.True<ClothingItem>();

        if (!string.IsNullOrWhiteSpace(parameters.Color))
        {
            var color = parameters.Color.ToLower();
            predicate = predicate.And(c => c.Color.ToLower() == color);
        }

        if (!string.IsNullOrWhiteSpace(parameters.CategoryName))
        {
            var category = parameters.CategoryName.ToLower();
            predicate = predicate.And(c => c.Category != null && c.Category.CategoryName.ToLower() == category);
        }

        if (!string.IsNullOrWhiteSpace(parameters.TypeName))
        {
            var type = parameters.TypeName.ToLower();
            predicate = predicate.And(c => c.Type != null && c.Type.TypeName.ToLower() == type);
        }

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

        query = query.Where(predicate);
    }
    
    
    public async Task<ClothingItem?> GetClothingItemDetailsAsync(int idItems)
    {
        return await context.ClothingItems
            .Include(c => c.Type)
            .Include(c => c.Category)
            .Include(c => c.TemperatureSuitability)
            .Include(c => c.Styles).ThenInclude(cs => cs.Style)
            .Include(c => c.Seasons).ThenInclude(cs => cs.Season)
            .FirstOrDefaultAsync(c => c.Id == idItems);
    }
}