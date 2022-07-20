namespace PersonFit.Core.Commands;

public interface ICommandHandler<in T> where T : ICommand 
{
    Task HandleAsync(T command, CancellationToken token = default);
}