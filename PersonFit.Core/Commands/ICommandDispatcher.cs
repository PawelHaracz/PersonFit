namespace PersonFit.Core.Commands;

public interface ICommandDispatcher
{
    Task SendAsync<T>(T command, CancellationToken token = default) where T : ICommand;
}