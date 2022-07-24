
namespace PersonFit.Domain.Exercise.Infrastructure.Postgres.Repositories;
using PersonFit.Domain.Exercise.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using PersonFit.Core;
using Documents;

internal class ReadExerciseRepository : IReadExerciseRepository
{
    private readonly IPostgresRepository<ExerciseDocument, Guid> _postgresRepository;

    public ReadExerciseRepository(IPostgresRepository<ExerciseDocument, Guid> postgresRepository)
    {
        _postgresRepository = postgresRepository;
    }

    public async Task<IEnumerable<Core.Entities.Exercise>> GetAll(CancellationToken token = default)
    {
        var items = await _postgresRepository.Get().ToListAsync(token);

        return items.Select(i => i.AsEntity());
    }
}