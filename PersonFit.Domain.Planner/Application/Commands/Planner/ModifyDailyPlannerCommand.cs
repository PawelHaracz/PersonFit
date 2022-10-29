namespace PersonFit.Domain.Planner.Application.Commands.Planner;
using PersonFit.Core.Commands;
using PersonFit.Core.Enums;
using Dtos;
internal record ModifyDailyPlannerCommand(Guid PlannerId, Guid OwnerId, DayOfWeek DayOfWeek, TimeOfDay TimeOfDay, IEnumerable<DailyPlannerModifierDto> Exercises): ICommand {}