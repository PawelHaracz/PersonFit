using PersonFit.Core;

namespace PersonFit.Domain.Exercise.Infrastructure.Dispatchers;

internal class CommandDispatcher: ICommandDispatcher
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public CommandDispatcher(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;

    }

    public async Task SendAsync<T>(T command, CancellationToken token = default) where T : ICommand
    {
        await using var scope = _serviceScopeFactory.CreateAsyncScope();
        await scope.ServiceProvider.GetRequiredService<ICommandHandler<T>>().HandleAsync(command, token);
    }
}