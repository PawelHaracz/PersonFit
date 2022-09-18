namespace PersonFit.Infrastructure.Dispatchers;
using Microsoft.Extensions.DependencyInjection;
using Core.Queries;

internal sealed class QueryDispatcher : IQueryDispatcher
{
    private readonly IServiceScopeFactory _serviceFactory;

    public QueryDispatcher(IServiceScopeFactory serviceFactory)
    {
        _serviceFactory = serviceFactory;
    }

    public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken token = default)
    {
        using var scope = _serviceFactory.CreateScope();
        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
        dynamic handler = scope.ServiceProvider.GetRequiredService(handlerType);

        return await handler.HandleAsync((dynamic)query, token);
    }

    public async Task<TResult> QueryAsync<TQuery, TResult>(TQuery query, CancellationToken token = default) where TQuery : class, IQuery<TResult>
    {
        using var scope = _serviceFactory.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();

        return await handler.HandleAsync(query, token);
    }
}