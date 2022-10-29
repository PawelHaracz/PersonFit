using PersonFit.Core.Events;
using PersonFit.Core.Enums;

namespace PersonFit.Domain.Planner.Core.Events;

internal record RemovedDailyPlannerEvent(DayOfWeek DayOfWeek, TimeOfDay TimeOfDay): IDomainEvent {}