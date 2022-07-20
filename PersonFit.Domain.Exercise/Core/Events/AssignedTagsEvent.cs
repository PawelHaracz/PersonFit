using PersonFit.Core;
using PersonFit.Core.Events;

namespace PersonFit.Domain.Exercise.Core.Events;

internal record AssignedTagsEvent(string[] Tags): IDomainEvent;