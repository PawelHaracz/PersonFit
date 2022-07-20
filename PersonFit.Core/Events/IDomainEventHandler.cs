namespace PersonFit.Core.Events;

public interface IDomainEventHandler<in T> where T : class, IDomainEvent
{
    Task HandleAsync(T @event);
}