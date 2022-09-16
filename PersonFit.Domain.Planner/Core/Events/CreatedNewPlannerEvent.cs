namespace PersonFit.Domain.Planner.Core.Events;
using PersonFit.Core.Events;
using Enums;

internal record CreatedNewPlannerEvent(Guid Id, Guid OwnerId, DateTime StartTime, DateTime EndTime, PlannerStatus Status) : IDomainEvent
{
}