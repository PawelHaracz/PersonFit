namespace PersonFit.Core.Events;

public interface IEventMapper
{
    IntegrationEvent Map(IDomainEvent @event);
    IEnumerable<IntegrationEvent> MapAll(IEnumerable<IDomainEvent> events);
}