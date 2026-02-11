namespace DAL.Helpers.Search;

public interface ISearchHelper<T> where T : class
{
    IQueryable<T> ApplySearch(IQueryable<T> entities, string searchQuery, IEnumerable<string> searchFields);
}