using PersonFit.Core.Exceptions;

namespace PersonFit.Domain.Planner.Application.Exceptions;

internal class PlannerAlreadyCreatedException : AppException
{
    public Guid OwnerId { get; }
    public DateOnly StartTime { get; }
    public DateOnly EndTime { get; }
    public Guid PlannerId { get; }
    public override string Code { get; } = "planner_already_created";

    public PlannerAlreadyCreatedException(Guid ownerId, DateOnly startTime, DateOnly endTime, Guid plannerId) : 
        base($"Owner {ownerId} has already activated or pending planner {plannerId} in selected date {startTime} to {endTime}")
    {
        OwnerId = ownerId;
        StartTime = startTime;
        EndTime = endTime;
        PlannerId = plannerId;
    }
}