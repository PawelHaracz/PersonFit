namespace PersonFit.Domain.Exercise.Infrastructure.Postgres;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PersonFit.Core;
using Exceptions;
using Documents;

[Obsolete]
internal class ExercisePostgresRepository : IPostgresRepository<ExerciseDocument, Guid>
{
    private readonly PostgresExerciseDomainContext _exerciseDomainContext;

    public ExercisePostgresRepository(PostgresExerciseDomainContext exerciseDomainContext)
    {
        _exerciseDomainContext = exerciseDomainContext;
    }

    public async Task<ExerciseDocument> GetAsync(Guid id, CancellationToken token = default)
    {
        var exercise = await _exerciseDomainContext.Exercises.SingleOrDefaultAsync(document => document.Id == id, token);
        if (exercise is null)
        {
            throw new ExerciseNotExistDatabaseException(id);
        }

        return exercise;
    }

    public async Task<ExerciseDocument> GetAsync(Expression<Func<ExerciseDocument, bool>> predicate,
        CancellationToken token = default)
    {
        var exercise = await _exerciseDomainContext.Exercises.SingleOrDefaultAsync(predicate, token);

        return exercise;
    }

    public async Task AddAsync(ExerciseDocument entity, CancellationToken token = default)
    {
        await _exerciseDomainContext.Exercises.AddAsync(entity, token);
        await _exerciseDomainContext.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(ExerciseDocument entity, CancellationToken token = default)
    {
        await _exerciseDomainContext.Exercises.AddAsync(entity, token);
        await _exerciseDomainContext.SaveChangesAsync(token);
    }

    public async Task DeleteAsync(Guid id, CancellationToken token = default)
    {
        var exercise = await _exerciseDomainContext.Exercises.SingleOrDefaultAsync(document => document.Id == id, token);
        if (exercise is null)
        {
            return;
        }

        _exerciseDomainContext.Exercises.Remove(exercise);
        await _exerciseDomainContext.SaveChangesAsync(token);
    }

    public async Task<bool> ExistsAsync(Expression<Func<ExerciseDocument, bool>> predicate,
        CancellationToken token = default)
    {
        var exercise = await GetAsync(predicate, token);
        return exercise is not null;
    }

    public async Task<IEnumerable<ExerciseDocument>> Get(Expression<Func<ExerciseDocument, bool>> predicate, CancellationToken token = default)
    {
        var list = await GetQueryable(predicate).ToListAsync(token);

        return list;
    }

    public IQueryable<ExerciseDocument> Get() 
        => _exerciseDomainContext.Exercises.AsQueryable();

    public IQueryable<ExerciseDocument> GetQueryable(Expression<Func<ExerciseDocument, bool>> predicate)
    {
        if (predicate is null)
        {
            return Get().AsNoTracking();
        }
        
        return  _exerciseDomainContext.Exercises.Where(predicate).AsQueryable().AsNoTracking();
    }
}
    