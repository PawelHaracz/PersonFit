namespace PersonFit.Domain.Planner.Api.Dtos.Commands.Planner;

public record CreatePlannerCommandDto(DateOnly StartTime, DateOnly EndTime)
{
}