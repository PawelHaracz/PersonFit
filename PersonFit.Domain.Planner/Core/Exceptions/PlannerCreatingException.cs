namespace PersonFit.Domain.Planner.Core.Exceptions;
using PersonFit.Core.Exceptions;
using Humanizer;

internal class PlannerCreatingException : DomainException
{
    public DateTime StartTime { get; }
    public DateTime EndTime { get; }
    public Guid UserId { get; }
    public override string Code { get; } = "cannot_create_planner_in_date_range";

    public PlannerCreatingException(Guid userId, DateTime startTime, DateTime endTime) : 
        base($"Cannot create the planner for the user {userId} in these date range {startTime.Humanize()} - {endTime.Humanize()}")
    {
        UserId = userId;
        StartTime = startTime;
        EndTime = endTime;
    }
}