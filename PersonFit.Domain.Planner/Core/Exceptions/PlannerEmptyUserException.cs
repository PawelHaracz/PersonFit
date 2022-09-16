namespace PersonFit.Domain.Planner.Core.Exceptions;
using PersonFit.Core.Exceptions;

public class PlannerEmptyUserException : DomainException
{
    public override string Code { get; } = "planner_cannot_create_empty_user_id";

    public PlannerEmptyUserException() : base("Cannot create planner, because user is not authorized")
    {
    }
}