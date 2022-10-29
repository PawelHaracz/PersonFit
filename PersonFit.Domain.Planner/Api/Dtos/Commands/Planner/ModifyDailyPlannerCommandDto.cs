namespace PersonFit.Domain.Planner.Api.Dtos.Commands.Planner;
using PersonFit.Core.Enums;
public record ModifyDailyPlannerCommandDto(DayOfWeek DayOfWeek, TimeOfDay TimeOfDay, IEnumerable<Guid> ToAdd, IEnumerable<Guid> ToRemove){}