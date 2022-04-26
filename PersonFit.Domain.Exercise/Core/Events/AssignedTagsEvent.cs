using PersonFit.Core;

namespace PersonFit.Domain.Exercise.Core.Events;

internal record AssignedTagsEvent(string[] Tags): IDomainEvent;
internal record UnassignedTagsEvent(string[] Tags): IDomainEvent;