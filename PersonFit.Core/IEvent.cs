namespace PersonFit.Core;

public interface IEvent
{
    Guid Id { get; }
    TimeSpan TimeSpan { get; }
}