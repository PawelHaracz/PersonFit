namespace PersonFit.Domain.Planner.Core.Exceptions;
using PersonFit.Core.Exceptions;

internal class ReorderingExerciseRepetitionException : DomainException
{
    public override string Code { get; } = "remove_exercise_repetition_error";
    public Guid Id { get; }
    public int OldOrder { get; }
    public int NewOrder { get; }
    
    public ReorderingExerciseRepetitionException(Guid id, int oldOrder, int newOrder) : base($"can't change order {oldOrder} to {newOrder} for planning exercise : {id}")
        => (Id, OldOrder, NewOrder) = (id, oldOrder, newOrder);
}