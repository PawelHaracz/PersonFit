namespace PersonFit.Domain.Planner.Core.Exceptions;
using PersonFit.Core.Exceptions;

internal class InvalidExerciseIdPlannerExerciseException: DomainException
{
    public override string Code { get; } = "empty_exercise_id_planner_exercise";
    public InvalidExerciseIdPlannerExerciseException() : base("Empty exercise id ")
    {
    }
}