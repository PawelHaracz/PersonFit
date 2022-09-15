namespace PersonFit.Domain.Planner.Core.Events;
using PersonFit.Core.Events;

internal record AddedPlannerExercise(Guid PlannerExercise): IDomainEvent {}