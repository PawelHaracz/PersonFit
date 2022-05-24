using Dapr.Client;
using PersonFit.Core;

namespace PersonFit.Domain.Exercise.Infrastructure.Events;

internal class EventProcessor: IEventProcessor
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IEventMapper _eventMapper;
    private readonly IMessageBroker _messageBroker;
    private readonly ILogger<IEventProcessor> _logger;

    public EventProcessor(IServiceScopeFactory serviceScopeFactory, IEventMapper eventMapper,
        IMessageBroker messageBroker, ILogger<EventProcessor> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _eventMapper = eventMapper;
        _messageBroker = messageBroker;
        _logger = logger;
    }
    public async Task ProcessAsync(IEnumerable<IDomainEvent> events, CancellationToken token = default)
    {
        if (events is null)
        {
            return;
        }

        _logger.LogTrace("Processing domain events...");
        var integrationEvents = await HandleDomainEvents(events);
        if (!integrationEvents.Any())
        {
            return;
        }

        _logger.LogTrace("Processing integration events...");
        await _messageBroker.PublishAsync(integrationEvents, token);
    }

    private async Task<List<IEvent>> HandleDomainEvents(IEnumerable<IDomainEvent> events)
    {
        var integrationEvents = new List<IEvent>();
        using var scope = _serviceScopeFactory.CreateScope();
        foreach (var @event in events)
        {
            var eventType = @event.GetType();
            _logger.LogTrace("Handling domain event: {@eventType.Name}", eventType.Name);
            var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(eventType);
            dynamic handlers = scope.ServiceProvider.GetServices(handlerType);
             foreach (var handler in handlers)
             {
                 if (handler != null)
                 {
                     await handler.HandleAsync((dynamic)@event);
                 }
             }

            var integrationEvent = _eventMapper.Map(@event);
            if (integrationEvent is null)
            {
                continue;
            }

            integrationEvents.Add(integrationEvent);
        }

        return integrationEvents;
    }
}