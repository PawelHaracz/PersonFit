using PersonFit.Core;

namespace PersonFit.Domain.Exercise.Core.Events;

internal record MediaContentAddedEvent(Guid Id, string Type, string Url): IDomainEvent
{
}