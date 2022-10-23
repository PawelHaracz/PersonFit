namespace PersonFit.Domain.Planner.Application.Policies;
using PersonFit.Core;
using Core.Enums;
internal class PlannerPolicyEvaluator : IPolicyEvaluator<Core.Entities.Planner>
{
    private readonly PlannerStatus[] _allowedStatus = {
        PlannerStatus.Activate,
        PlannerStatus.Pending
    };
    
    public Task<bool> Evaluate(Core.Entities.Planner aggregate, CancellationToken token = default)
    {
        return Task.FromResult( _allowedStatus.Contains(aggregate.Status));
    }
}