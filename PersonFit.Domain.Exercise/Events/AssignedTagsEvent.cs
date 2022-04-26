namespace PersonFit.Domain.Exercise.Events;
using Core;

internal record AssignedTagsEvent(string[] Tags): IDomainEvent;
internal record UnassignedTagsEvent(string[] Tags): IDomainEvent;