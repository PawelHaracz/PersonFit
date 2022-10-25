namespace PersonFit.Domain.Planner.Api.Dtos.Commands.Planner;
using Core.Enums;

public record AddDailyPlannerCommandDto(DayOfWeek DayOfWeek, TimeOfDay TimeOfDay, IEnumerable<Guid> Workouts){}