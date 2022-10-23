namespace PersonFit.Domain.Planner.Application.Exceptions;
using PersonFit.Core.Exceptions;

internal class ExerciseAlreadyCreatedException: AppException
{
    public override string Code { get; } = "exercise_planner_already_created";
    public Guid ExercisePlannerId { get; }
    public Guid ExerciseId { get; }
    public Guid OwnerId { get; }
    
    public ExerciseAlreadyCreatedException(Guid ownerId, Guid exerciseId, Guid exercisePlannerId) : base(
        $"Owner {ownerId} already has exerciseId {exerciseId} in the planner {exercisePlannerId}")
        => (OwnerId, ExerciseId, ExercisePlannerId) = (ownerId, exerciseId, exercisePlannerId);
}