using PersonFit.Infrastructure.Exceptions;

namespace PersonFit.Domain.Planner.Infrastructure.Exceptions;

internal class PlannerDoesNotExists : InfraException
{
    public override string Code { get; } = "planner_does_not_exists_or_insufficient_permission";
    public Guid OwnerId { get; }
    public Guid PlannerId { get; }

    public PlannerDoesNotExists(Guid ownerId, Guid plannerId) : base($"Owner {ownerId} doesn't have permission to planner {plannerId} or record does not exists")
    {
        OwnerId = ownerId;
        PlannerId = plannerId;
    }
}