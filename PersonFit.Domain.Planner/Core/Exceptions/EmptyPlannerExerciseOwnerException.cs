using PersonFit.Core.Exceptions;

namespace PersonFit.Domain.Planner.Core.Exceptions;

public class EmptyPlannerExerciseOwnerException : DomainException
{
    public override string Code { get; } = "empty_planner_exercise_owner";

    public Guid ExerciseId { get; set; }

    public EmptyPlannerExerciseOwnerException(Guid exerciseId) : base("Exercise planner doesn't have owner id")
        => ExerciseId = exerciseId;
}