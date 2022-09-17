using PersonFit.Core;
using PersonFit.Domain.Planner.Core.Entities;
using PersonFit.Domain.Planner.Core.Repositories;
using PersonFit.Domain.Planner.Infrastructure.Postgres.Documents;

namespace PersonFit.Domain.Planner.Infrastructure.Postgres.Repositories;

internal class ExercisePlannerDomainRepository : IExerciseRepository
{
    private readonly IPostgresRepository<ExercisePlannerDocument, Guid> _postgresRepository;
    
    public async Task<Guid> Exists(Guid ownerId, Guid exerciseId, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public async Task Create(PlannerExercise exercise, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public async Task<PlannerExercise> Get(Guid exerciseId, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public async Task Update(PlannerExercise exercise, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}