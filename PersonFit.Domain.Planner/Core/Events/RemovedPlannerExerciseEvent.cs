namespace PersonFit.Domain.Planner.Core.Events;
using PersonFit.Core.Events;
using Enums;

internal record RemovedPlannerExerciseEvent(DayOfWeek DayOfWeek, TimeOfDay TimeOfDay, Guid PlannerExercise): IDomainEvent {}