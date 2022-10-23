namespace PersonFit.Domain.Planner.Core.Repositories;

internal interface IPlannerRepository
{
    Task<Guid> GetActivatedPlannerId(Guid ownerId, DateTime starTime, DateTime endTime, CancellationToken token = default);
    Task Create(Core.Entities.Planner planner, CancellationToken token = default);
    Task<Entities.Planner> GetById(Guid ownerId, Guid id, CancellationToken token = default);
    Task Update(Core.Entities.Planner planner, CancellationToken token = default);
}