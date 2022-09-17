namespace PersonFit.Domain.Planner.Core.Repositories;

internal interface IExerciseRepository
{
    Task<Guid> Exists(Guid ownerId, Guid exerciseId, CancellationToken token);
    Task Create(Entities.PlannerExercise exercise, CancellationToken token);
    Task<Entities.PlannerExercise> Get(Guid exerciseId, CancellationToken token);
    Task Update(Entities.PlannerExercise exercise, CancellationToken token);
}