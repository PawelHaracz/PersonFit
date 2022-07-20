namespace PersonFit.Core.Events;

public interface IMessageBroker
{
    Task PublishAsync(params IntegrationEvent[] events);
    Task PublishAsync(IEnumerable<IntegrationEvent> events, CancellationToken token);
}