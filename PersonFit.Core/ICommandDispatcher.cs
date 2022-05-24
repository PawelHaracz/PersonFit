namespace PersonFit.Core;

public interface ICommandDispatcher
{
    Task SendAsync<T>(T command, CancellationToken token = default) where T : ICommand;
}