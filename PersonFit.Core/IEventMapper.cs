namespace PersonFit.Core;

public interface IEventMapper
{
    IEvent Map(IDomainEvent @event);
    IEnumerable<IEvent> MapAll(IEnumerable<IDomainEvent> events);
}