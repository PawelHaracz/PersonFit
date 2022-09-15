namespace PersonFit.Core.Events;

public interface IAcceptedEvent : IEvent
{
    Guid Id { get; }
    TimeSpan TimeSpan { get; }
}