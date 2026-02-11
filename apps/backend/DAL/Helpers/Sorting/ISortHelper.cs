namespace DAL.Helpers.Sorting;

public interface ISortHelper<T>
{
    IQueryable<T> ApplySort(IQueryable<T> entities, string orderByQueryString);
}