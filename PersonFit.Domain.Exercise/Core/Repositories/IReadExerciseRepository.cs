namespace PersonFit.Domain.Exercise.Core.Repositories;

internal interface IReadExerciseRepository
{
    Task<IEnumerable<Core.Entities.Exercise>> GetAll(CancellationToken token = default);

    Task<Core.Entities.Exercise> Get(Guid id, CancellationToken token = default);
}