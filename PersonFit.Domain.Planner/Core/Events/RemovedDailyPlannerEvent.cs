using PersonFit.Core.Events;
using PersonFit.Domain.Planner.Core.Enums;

namespace PersonFit.Domain.Planner.Core.Events;

internal record RemovedDailyPlannerEvent(DayOfWeek DayOfWeek, TimeOfDay TimeOfDay): IDomainEvent {}