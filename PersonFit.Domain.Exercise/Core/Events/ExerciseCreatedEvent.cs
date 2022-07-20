using PersonFit.Core;
using PersonFit.Core.Events;

namespace PersonFit.Domain.Exercise.Core.Events;

internal record ExerciseCreatedEvent(Guid Id, string Name, string Description) : IDomainEvent
{
    
}