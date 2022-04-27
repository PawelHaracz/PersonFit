namespace PersonFit.Domain.Exercise.Infrastructure.Postgres.Repositories;
using PersonFit.Core;
using Documents;

internal class ExerciseDomainRepository : IDomainRepository<Core.Entities.Exercise>
{
    private readonly IPostgresRepository<ExerciseDocument, Guid> _repositoryActor;

    public ExerciseDomainRepository(IPostgresRepository<ExerciseDocument, Guid> repositoryActor)
    {
        _repositoryActor = repositoryActor;
    }
    public Task<Core.Entities.Exercise> GetAsync(AggregateId id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(AggregateId id)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Core.Entities.Exercise resource)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Core.Entities.Exercise resource)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(AggregateId id)
    {
        throw new NotImplementedException();
    }
    
}