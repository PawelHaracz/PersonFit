namespace PersonFit.Core.Queries;

public interface IQueryHandler<in TQuery,TResult> where TQuery : class, IQuery<TResult>
{
    Task<TResult> HandleAsync(TQuery query, CancellationToken token = default);
}