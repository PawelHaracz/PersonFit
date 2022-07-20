namespace PersonFit.Infrastructure.Events;

using Microsoft.Extensions.Logging;
using PersonFit.Core.Events;
internal class MessageBroker : IMessageBroker
{
    private readonly ILogger<MessageBroker> _logger;

    public MessageBroker(ILogger<MessageBroker> logger)
    {
        _logger = logger;
    }
    
    public Task PublishAsync(params IntegrationEvent[] events) => PublishAsync(events?.AsEnumerable(), CancellationToken.None);

    public async Task PublishAsync(IEnumerable<IntegrationEvent> events, CancellationToken token = default)
    {
        if (events is null)
        {
            return;
        }

        foreach (var @event in events)
        {
            if (@event is null)
            {
                continue;
            }
            _logger.LogTrace("publishing event {@event}", @event.Event.Id);
            // todo improve generic topicname and metadata
            //await _daprClient.PublishEventAsync("pubsub", @event.TopicName, @event, new Dictionary<string, string>() { }, token);
            _logger.LogTrace("published event {@event}", @event.Event.Id);
        }
    }
}