namespace PersonFit.Core;

public interface IDomainRepository<TEntity>
{
    Task<TEntity> GetAsync(AggregateId id);
    Task<bool> ExistsAsync(AggregateId id);
    Task AddAsync(TEntity resource);
    Task UpdateAsync(TEntity resource);
    Task DeleteAsync(AggregateId id);
}