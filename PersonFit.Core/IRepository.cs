namespace PersonFit.Core;

public interface IRepository<TEntity>
{
    Task<TEntity> GetAsync(AggregateId id);
    Task<bool> ExistsAsync(AggregateId id);
    Task AddAsync(TEntity resource);
    Task UpdateAsync(TEntity resource);
    Task DeleteAsync(AggregateId id);
}