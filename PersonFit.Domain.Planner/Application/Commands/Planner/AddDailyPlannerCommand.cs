namespace PersonFit.Domain.Planner.Application.Commands.Planner;
using PersonFit.Core.Commands;
using PersonFit.Core.Enums;

internal record AddDailyPlannerCommand(Guid PlannerId, Guid OwnerId, DayOfWeek DayOfWeek, TimeOfDay TimeOfDay, IEnumerable<Guid> Exercises): ICommand {}