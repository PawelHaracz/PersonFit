using PersonFit.Core;

namespace PersonFit.Domain.Exercise.Core.Events;

public record MediaContentAddedEvent(Guid Id, string Type, string Url): IDomainEvent
{
}