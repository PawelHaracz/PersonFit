namespace PersonFit.Core;

public interface IEventMapper
{
    IntegrationEvent Map(IDomainEvent @event);
    IEnumerable<IntegrationEvent> MapAll(IEnumerable<IDomainEvent> events);
}