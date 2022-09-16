namespace PersonFit.Domain.Planner.Core.Events;
using PersonFit.Core.Events;
using Enums;

internal record RemovedPlannerExercise(DayOfWeek DayOfWeek, TimeOfDay TimeOfDay, Guid PlannerExercise): IDomainEvent {}