using PersonFit.Core.Aggregations;
using PersonFit.Core.Exceptions;

namespace PersonFit.Domain.Planner.Core.Exceptions;

internal class MissingOrderKeyException : DomainException
{
    public override string Code { get; } = "missing_order_key_during_reordering_exercise_repetition_error";
    public Guid Id { get; }
    public int Order { get; }
    
    public MissingOrderKeyException(Guid id, int order) : base($"can't reorder exercise repetitions because missing order {order} in reordering model for planning exercise : {id}")
        => (Id, Order) = (id, order);
}