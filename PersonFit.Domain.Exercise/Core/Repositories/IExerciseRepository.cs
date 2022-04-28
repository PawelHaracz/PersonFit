namespace PersonFit.Domain.Exercise.Core.Repositories;

public interface IExerciseRepository
{
    Task<bool> Exist(string name, CancellationToken token);

    Task Create(Entities.Exercise exercise, CancellationToken token);
}