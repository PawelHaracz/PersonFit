using System.Linq.Expressions;
using Dapr.Actors.Runtime;

namespace PersonFit.Core;

public interface IPostgresRepository<TEntity, in TIdentifiable> where TEntity: IIdentifiable<TIdentifiable>
{
    Task<TEntity> GetAsync(TIdentifiable id, CancellationToken token = default);
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken token = default);
    Task AddAsync(TEntity entity, CancellationToken token = default);
    Task UpdateAsync(TEntity entity, CancellationToken token = default);
    Task DeleteAsync(TIdentifiable id, CancellationToken token = default);
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken token = default);
}