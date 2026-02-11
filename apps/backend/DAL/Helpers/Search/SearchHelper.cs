using System.Linq.Dynamic.Core;

namespace DAL.Helpers.Search;

public class SearchHelper<T> : ISearchHelper<T> where T : class
{
    public IQueryable<T> ApplySearch(IQueryable<T> entities, string searchQuery, IEnumerable<string> searchFields)
    {
        if (!entities.Any() || string.IsNullOrWhiteSpace(searchQuery) || searchFields == null || !searchFields.Any())
        {
            return entities;
        }

        searchQuery = searchQuery.Trim().ToLower();
        
        var searchExpressions = searchFields
            .Select(field => $"{field}.ToLower().Contains(\"{searchQuery}\")")
            .ToList();
        
        var finalSearchExpression = string.Join(" || ", searchExpressions);

        return entities.Where(finalSearchExpression);
    }
}