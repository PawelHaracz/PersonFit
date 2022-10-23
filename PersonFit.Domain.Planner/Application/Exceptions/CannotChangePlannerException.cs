using PersonFit.Core.Exceptions;

namespace PersonFit.Domain.Planner.Application.Exceptions;

internal class CannotChangePlannerException : AppException
{
    public override string Code { get; } = "cannot_change_planner";
    public Guid PlannerId { get; }
    public string HandlerName { get; }

    public CannotChangePlannerException(Guid plannerId, string handlerName) : base($"{handlerName} tried to perform action for planner {plannerId} but cannot do it")
    {
        PlannerId = plannerId;
        HandlerName = handlerName;
    }
}