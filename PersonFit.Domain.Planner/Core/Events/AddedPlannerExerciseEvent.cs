namespace PersonFit.Domain.Planner.Core.Events;
using PersonFit.Core.Events;
using PersonFit.Core.Enums;
internal record AddedPlannerExerciseEvent(DayOfWeek DayOfWeek, TimeOfDay TimeOfDay, Guid PlannerExercise): IDomainEvent {}