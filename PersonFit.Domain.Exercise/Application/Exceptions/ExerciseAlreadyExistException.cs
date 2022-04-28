namespace PersonFit.Domain.Exercise.Application.Exceptions;

public class ExerciseAlreadyCreatedException: AppException
{
    public override string Code { get; } = "exercise_already_created";
    public string Name { get; }
    public ExerciseAlreadyCreatedException(string name) : base($"Exercise already created {name}. You can't create exercise with the same name.")
    {
        Name = name;
    }
}