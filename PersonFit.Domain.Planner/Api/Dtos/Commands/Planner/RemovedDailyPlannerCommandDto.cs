namespace PersonFit.Domain.Planner.Api.Dtos.Commands.Planner;
using Core.Enums;
public record RemovedDailyPlannerCommandDto(DayOfWeek DayOfWeek, TimeOfDay TimeOfDay){}