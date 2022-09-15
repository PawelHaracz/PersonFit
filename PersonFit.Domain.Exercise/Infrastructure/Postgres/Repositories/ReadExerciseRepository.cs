namespace PersonFit.Domain.Exercise.Infrastructure.Postgres.Repositories;
using PersonFit.Domain.Exercise.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using PersonFit.Core;
using Documents;
using Microsoft.Extensions.Logging;

internal class ReadExerciseRepository : IReadExerciseRepository
{
    private readonly IPostgresRepository<ExerciseDocument, Guid> _postgresRepository;
    private readonly ILogger _logger;

    public ReadExerciseRepository(IPostgresRepository<ExerciseDocument, Guid> postgresRepository, ILogger<ReadExerciseRepository> logger)
    {
        _postgresRepository = postgresRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Core.Entities.Exercise>> GetAll(CancellationToken token = default)
    {
        var items = await _postgresRepository.Get().ToListAsync(token);
        return items.Select(i => i.AsEntity());
    }

    public async Task<Core.Entities.Exercise> Get(Guid id, CancellationToken token = default)
    {
        var items = await _postgresRepository.Get(document => document.Id == id, token);
        var item = items.SingleOrDefault();
        if (item is null)
        {
            _logger.LogWarning("Exercise by {id} doesn't exist", id);
            return default;
        }

        return item.AsEntity();
    }
}