namespace PersonFit.Domain.Planner.Infrastructure.Postgres.Repositories;
using PersonFit.Core;
using PersonFit.Domain.Planner.Core.Repositories;
using Documents;

internal class PlannerDomainRepository : IPlannerRepository
{
    private readonly IPostgresRepository<PlannerDocument, Guid> _postgresRepository;

    public PlannerDomainRepository(IPostgresRepository<PlannerDocument, Guid> postgresRepository)
    {
        _postgresRepository = postgresRepository;
    }
}