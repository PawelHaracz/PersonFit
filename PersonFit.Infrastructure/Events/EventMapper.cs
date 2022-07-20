using PersonFit.Core.Events;

namespace PersonFit.Infrastructure.Events;

internal class EventMapper : IEventMapper
{
    public IntegrationEvent Map(IDomainEvent @event)
        => @event switch
        {
            // UnassignedTagsEvent e => new IntegrationEvent(null, String.Empty),
            // AssignedTagsEvent e => new IntegrationEvent(null, String.Empty),
            // MediaContentAddedEvent e => new IntegrationEvent(null, String.Empty),
            // ExerciseCreatedEvent e => new IntegrationEvent(null, String.Empty),
            _ => new IntegrationEvent(null, String.Empty),
        };

    public IEnumerable<IntegrationEvent> MapAll(IEnumerable<IDomainEvent> events)
        => events.Select(Map);
}