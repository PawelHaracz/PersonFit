namespace PersonFit.Domain.Planner.Core.Events;
using PersonFit.Core.Events;
using Enums;
internal record AddedPlannerExerciseEvent(DayOfWeek DayOfWeek, TimeOfDay TimeOfDay, Guid PlannerExercise): IDomainEvent {}