namespace PersonFit.Domain.Planner.Core.Exceptions;
using PersonFit.Core.Exceptions;

internal class RemoveExerciseRepetitionException : DomainException
{
    public override string Code { get; } = "remove_exercise_repetition_error";
    public Guid Id { get; }
    public int Order { get; }
    
    public RemoveExerciseRepetitionException(Guid id, int order) : base($"can't remove exercise repetition ordered {order}  for planning exercise : {id}")
        => (Id, Order) = (id, order);
}