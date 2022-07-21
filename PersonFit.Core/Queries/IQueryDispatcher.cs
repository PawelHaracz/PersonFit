namespace PersonFit.Core.Queries;

public interface IQueryDispatcher
{
    Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken token = default);
    Task<TResult> QueryAsync<TQuery, TResult>(TQuery query, CancellationToken token = default) where TQuery : class, IQuery<TResult>;
}