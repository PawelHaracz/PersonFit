

using Dapr.Client;

namespace PersonFit.Domain.Exercise.Infrastructure.Events;
using PersonFit.Core;

internal class MessageBroker : IMessageBroker
{
    private readonly DaprClient _daprClient;
    private readonly ILogger<MessageBroker> _logger;

    public MessageBroker(DaprClient daprClient, ILogger<MessageBroker> logger)
    {
        _daprClient = daprClient;
        _logger = logger;
    }
    
    public Task PublishAsync(params IEvent[] events) => PublishAsync(events?.AsEnumerable(), CancellationToken.None);

    public async Task PublishAsync(IEnumerable<IEvent> events, CancellationToken token = default)
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
            _logger.LogTrace("publishing event {@event}", @event.Id);
            // todo improve generic topicname and metadata
            await _daprClient.PublishEventAsync("pubsub", "Exercise", @event, new Dictionary<string, string>() { }, token);
            _logger.LogTrace("published event {@event}", @event.Id);
        }
    }
}