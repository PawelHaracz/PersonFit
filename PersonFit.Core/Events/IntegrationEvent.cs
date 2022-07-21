namespace PersonFit.Core.Events;

public record IntegrationEvent(IAcceptedEvent Event, string TopicName)
{
    public static IntegrationEvent Empty = new (default, "");
};