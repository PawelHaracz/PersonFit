using PersonFit.Core;
using PersonFit.Core.Events;

namespace PersonFit.Domain.Exercise.Core.Events;

internal record MediaContentAddedEvent(Guid Id, string Type, string Url): IDomainEvent
{
}