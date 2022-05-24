using PersonFit.Core;
using PersonFit.Domain.Exercise.Core.Events;

namespace PersonFit.Domain.Exercise.Infrastructure.Events;

internal class EventMapper : IEventMapper
{
    public IEvent Map(IDomainEvent @event)
        => @event switch
        {
            UnassignedTagsEvent e => null,
            AssignedTagsEvent e => null,
            MediaContentAddedEvent e => null,
            ExerciseCreatedEvent e => null,
            _ => null
        };

    public IEnumerable<IEvent> MapAll(IEnumerable<IDomainEvent> events)
        => events.Select(Map);
}