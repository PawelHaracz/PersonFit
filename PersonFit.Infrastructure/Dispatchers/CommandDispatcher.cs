namespace PersonFit.Infrastructure.Dispatchers;
using System.Transactions;
using Microsoft.Extensions.DependencyInjection;
using Core.Commands;
internal class CommandDispatcher: ICommandDispatcher
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public CommandDispatcher(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;

    }

    public async Task SendAsync<T>(T command, CancellationToken token = default) where T : ICommand
    {
        using var transactionScope = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
            }, 
            TransactionScopeAsyncFlowOption.Enabled);
        
        await using var scope = _serviceScopeFactory.CreateAsyncScope();
        await scope.ServiceProvider.GetRequiredService<ICommandHandler<T>>().HandleAsync(command, token);
        transactionScope.Complete();
    }
}