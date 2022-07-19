namespace PersonFit.Core;

public interface IDomainEventHandler<in T> where T : class, IDomainEvent
{
    Task HandleAsync(T @event);
}