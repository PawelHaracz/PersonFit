namespace PersonFit.Domain.Planner.Infrastructure.Postgres.Repositories;
using Microsoft.EntityFrameworkCore;
using PersonFit.Core;
using Core.Entities;
using PersonFit.Domain.Planner.Core.Repositories;
using Documents;

internal class ExercisePlannerDomainRepository : IExerciseRepository
{
    private readonly IPostgresRepository<ExercisePlannerDocument, Guid> _postgresRepository;
    
    public async Task<Guid> Exists(Guid ownerId, Guid exerciseId, CancellationToken token)
    {
        var item = await _postgresRepository.GetQueryable(document =>
            document.ExerciseId == exerciseId && document.OwnerId == ownerId)
            .Select(document => document.Id)
            .FirstOrDefaultAsync(token);

        return item;
    }

    public async Task Create(PlannerExercise exercise, CancellationToken token)
    {
        var dao = exercise.AsDocument();

        await _postgresRepository.AddAsync(dao, token);
    }

    public async Task<PlannerExercise> Get(Guid exerciseId, CancellationToken token)
    {
        var item =  await _postgresRepository.GetAsync(exerciseId, token);

        return item.AsEntity();
    }

    public async Task Update(PlannerExercise exercise, CancellationToken token)
    {
        var dao = exercise.AsDocument();

        await _postgresRepository.UpdateAsync(dao, token);
    }
}