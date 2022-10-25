namespace PersonFit.Infrastructure.Postgres;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Core;
using Core.Aggregations;
using Exceptions;

public class PostgresDomainRepository<TEntity>: IPostgresRepository<TEntity, Guid> where TEntity : class, IIdentifiable<Guid>
{
    private readonly DbContext _context;

    public PostgresDomainRepository(DbContext context)
    {
        _context = context;
    }

    public async Task<TEntity> GetAsync(Guid id, CancellationToken token = default)
    {
        var entities = await _context.Set<TEntity>().FindAsync( id, token);
        if (entities is null)
        {
            throw new ItemNotExistDatabaseException(id, nameof(TEntity));
        }

        return entities;
    }

    public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken token = default)
        =>  GetQueryable(predicate).SingleOrDefaultAsync(token);

    public async Task AddAsync(TEntity entity, CancellationToken token = default)
    {
        await _context.Set<TEntity>().AddAsync(entity, token);
        await _context.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken token = default)
    {
         _  =  _context.Set<TEntity>().Update(entity);
         await _context.SaveChangesAsync(token);
    }

    public async Task DeleteAsync(Guid id, CancellationToken token = default)
    {
        var entity = await GetAsync(id, token);
        _context.Set<TEntity>().Remove(entity);
        await _context.SaveChangesAsync(token);
    }

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken token = default)
    {
        var entity = await GetAsync(predicate, token);

        return entity is not null;
    }

    public async Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> predicate, CancellationToken token = default)
    {
        var list = await GetQueryable(predicate).ToListAsync(token);

        return list;
    }

    public IQueryable<TEntity> Get() => _context.Set<TEntity>().AsNoTracking();

    public IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> predicate)
    {
        if (predicate is null)
        {
            return Get();
        }
        
        return  _context.Set<TEntity>().Where(predicate).AsQueryable().AsNoTracking();
    }
}