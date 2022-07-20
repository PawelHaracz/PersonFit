namespace PersonFit.Core.Events;

public interface IEvent
{
    Guid Id { get; }
    TimeSpan TimeSpan { get; }
}