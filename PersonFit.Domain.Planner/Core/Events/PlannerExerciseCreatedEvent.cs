namespace PersonFit.Domain.Planner.Core.Events;
using PersonFit.Core.Events;

internal record PlannerExerciseCreatedEvent(Guid Id, Guid OwnerId, Guid ExerciseId): IDomainEvent
{
}