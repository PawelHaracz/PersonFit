using PersonFit.Core;

namespace PersonFit.Domain.Exercise.Core.Events;

internal record ExerciseCreatedEvent(Guid Id, string Name, string Description) : IDomainEvent
{
    
}