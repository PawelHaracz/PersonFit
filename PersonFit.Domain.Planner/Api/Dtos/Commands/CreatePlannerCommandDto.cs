namespace PersonFit.Domain.Planner.Api.Dtos.Commands;

public record CreatePlannerCommandDto(DateOnly StartTime, DateOnly EndTime)
{
}