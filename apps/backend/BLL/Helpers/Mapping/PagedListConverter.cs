using AutoMapper;
using DAL.Helpers;

namespace BLL.Helpers.Mapping;

public class PagedListConverter<TSource, TDestination> : ITypeConverter<PagedList<TSource>, PagedList<TDestination>>
    where TSource : class
    where TDestination : class
{
    public PagedList<TDestination> Convert(PagedList<TSource> source, PagedList<TDestination> destination, ResolutionContext context)
    {
        var mappedItems = context.Mapper.Map<List<TDestination>>(source);
        return new PagedList<TDestination>(mappedItems, source.TotalCount, source.CurrentPage, source.PageSize);
    }
}