namespace PersonFit.Domain.Exercise.Core.Repositories;

internal interface IExerciseRepository
{
    Task<bool> Exist(string name, CancellationToken token);

    Task Create(Entities.Exercise exercise, CancellationToken token);
}