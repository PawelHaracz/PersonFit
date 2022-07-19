namespace PersonFit.Core;

public interface IMessageBroker
{
    Task PublishAsync(params IntegrationEvent[] events);
    Task PublishAsync(IEnumerable<IntegrationEvent> events, CancellationToken token);
}