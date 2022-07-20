namespace PersonFit.Core.Events;

public record IntegrationEvent(IEvent Event, string TopicName)
{
    public static IntegrationEvent Empty = new (default, "");
};