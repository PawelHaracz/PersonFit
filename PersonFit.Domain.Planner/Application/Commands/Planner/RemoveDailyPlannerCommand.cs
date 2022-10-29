namespace PersonFit.Domain.Planner.Application.Commands.Planner;
using PersonFit.Core.Commands;
using PersonFit.Core.Enums;

internal record RemoveDailyPlannerCommand(Guid PlannerId,  Guid OwnerId, DayOfWeek DayOfWeek, TimeOfDay TimeOfDay): ICommand {}