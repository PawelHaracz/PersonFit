namespace PersonFit.Domain.Planner.Core.Events;
using PersonFit.Core.Events;

internal record RemovedPlannerExercise(Guid PlannerExercise): IDomainEvent {}