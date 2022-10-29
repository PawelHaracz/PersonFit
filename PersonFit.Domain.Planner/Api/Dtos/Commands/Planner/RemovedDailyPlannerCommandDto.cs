namespace PersonFit.Domain.Planner.Api.Dtos.Commands.Planner;
using PersonFit.Core.Enums;
public record RemovedDailyPlannerCommandDto(DayOfWeek DayOfWeek, TimeOfDay TimeOfDay){}