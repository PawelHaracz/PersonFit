namespace PersonFit.Core;

public interface IEventProcessor
{
    Task ProcessAsync(IEnumerable<IDomainEvent> events, CancellationToken token = default);
}