namespace PersonFit.Core.Events;

public interface IEventProcessor
{
    Task ProcessAsync(IEnumerable<IDomainEvent> events, CancellationToken token = default);
}