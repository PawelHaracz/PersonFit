using PersonFit.Core.Events;
using PersonFit.Domain.Planner.Core.Enums;

namespace PersonFit.Domain.Planner.Core.Events;

internal record RemovedDailyPlanner(DayOfWeek DayOfWeek, TimeOfDay TimeOfDay): IDomainEvent {}