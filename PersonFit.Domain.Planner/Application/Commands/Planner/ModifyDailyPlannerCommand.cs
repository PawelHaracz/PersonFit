using PersonFit.Domain.Planner.Application.Dtos;

namespace PersonFit.Domain.Planner.Application.Commands.Planner;
using PersonFit.Core.Commands;
using Core.Enums;

internal record ModifyDailyPlannerCommand(Guid PlannerId, Guid OwnerId, DayOfWeek DayOfWeek, TimeOfDay TimeOfDay, IEnumerable<DailyPlannerModifierDto> Exercises): ICommand {}