namespace PersonFit.Domain.Exercise.Infrastructure.Postgres;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PersonFit.Core;
using Exceptions;
using Documents;

internal class ExercisePostgresRepository : IPostgresRepository<ExerciseDocument, Guid>
{
    private readonly PostgresContext _context;

    public ExercisePostgresRepository(PostgresContext context)
    {
        _context = context;
    }

    public async Task<ExerciseDocument> GetAsync(Guid id, CancellationToken token = default)
    {
        var exercise = await _context.Exercises.SingleOrDefaultAsync(document => document.Id == id, token);
        if (exercise is null)
        {
            throw new ExerciseNotExistDatabaseException(id);
        }

        return exercise;
    }

    public async Task<ExerciseDocument> GetAsync(Expression<Func<ExerciseDocument, bool>> predicate,
        CancellationToken token = default)
    {
        var exercise = await _context.Exercises.SingleOrDefaultAsync(predicate, token);

        return exercise;
    }

    public async Task AddAsync(ExerciseDocument entity, CancellationToken token = default)
    {
        await _context.Exercises.AddAsync(entity, token);
        await _context.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(ExerciseDocument entity, CancellationToken token = default)
    {
        await _context.Exercises.AddAsync(entity, token);
        await _context.SaveChangesAsync(token);
    }

    public async Task DeleteAsync(Guid id, CancellationToken token = default)
    {
        var exercise = await _context.Exercises.SingleOrDefaultAsync(document => document.Id == id, token);
        if (exercise is null)
        {
            return;
        }

        _context.Exercises.Remove(exercise);
        await _context.SaveChangesAsync(token);
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
        => _context.Exercises.AsQueryable();

    public IQueryable<ExerciseDocument> GetQueryable(Expression<Func<ExerciseDocument, bool>> predicate)
    {
        if (predicate is null)
        {
            return Get();
        }
        
        return  _context.Exercises.Where(predicate).AsQueryable();
    }
}
    