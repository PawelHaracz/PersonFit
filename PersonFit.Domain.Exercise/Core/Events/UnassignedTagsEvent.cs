using PersonFit.Core;

namespace PersonFit.Domain.Exercise.Core.Events;

internal record UnassignedTagsEvent(string[] Tags): IDomainEvent;