

namespace PersonFit.Domain.Exercise.Infrastructure.Postgres.Repositories;
using PersonFit.Core;
using Documents;
using PersonFit.Domain.Exercise.Core.Repositories;

internal class ExerciseDomainRepository : IExerciseRepository
{
    private readonly IPostgresRepository<ExerciseDocument, Guid> _postgresRepository;

    public ExerciseDomainRepository(IPostgresRepository<ExerciseDocument, Guid> postgresRepository)
    {
        _postgresRepository = postgresRepository;
    }

    public async Task<bool> Exist(string name, CancellationToken token)
    {
        var value = await _postgresRepository.ExistsAsync(i => i.Name == name, token);
        return value;
    }

    public async Task Create(Core.Entities.Exercise exercise, CancellationToken token)
    {
        await _postgresRepository.AddAsync(exercise.AsDocument(), token);
    }
}