namespace PersonFit.Domain.Planner.Infrastructure.Postgres.Repositories;
using PersonFit.Core;
using PersonFit.Domain.Planner.Core.Repositories;
using Documents;
using Microsoft.EntityFrameworkCore;
using Core.Enums;

internal class PlannerDomainRepository : IPlannerRepository
{
    private readonly IPostgresRepository<PlannerDocument, Guid> _postgresRepository;

    public PlannerDomainRepository(IPostgresRepository<PlannerDocument, Guid> postgresRepository)
    {
        _postgresRepository = postgresRepository;
    }

    public async Task<Guid> GetActivatedPlannerId(Guid ownerId, DateTime starTime, DateTime endTime, CancellationToken token = default)
    {
        var plannerId = await _postgresRepository.GetQueryable(document =>
                document.OwnerId == ownerId &&
                document.StartTime >= starTime.ToUniversalTime() &&
                document.EndTime <= endTime.ToUniversalTime() &&
                (document.Status == (int)PlannerStatus.Activate ||
                 document.Status == (int)PlannerStatus.Pending))
            .Select(document => document.Id)
            .SingleOrDefaultAsync(token);
        
        return plannerId;
    }

    public Task Create(Core.Entities.Planner planner, CancellationToken token = default)
    {
        var dao = planner.AsDocument();

        return _postgresRepository.AddAsync(dao, token);
    }
}