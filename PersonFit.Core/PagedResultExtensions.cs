namespace PersonFit.Core;
using Queries;

public static class PagedResultExtensions
{
    public static PagedResult<T> GetPaged<T>(this IQueryable<T> query, 
        int page, int pageSize, CancellationToken token = default) where T : class
    {
        var pageCount = (double)query.Count() / pageSize;
        var resultsPerPage =(int)Math.Ceiling(pageCount);
        var skip = (page - 1) * pageSize;
        var results = query.Skip(skip).Take(pageSize).ToList();//.ToListAsync(token); //coupling to the EF 6.0 because, would be better have async/await queries instead of synchronized calls 
        var result = PagedResult<T>.Create(results, page, resultsPerPage, query.Count(), pageSize);
        
        return result;
    }
}