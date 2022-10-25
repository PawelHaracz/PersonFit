using Microsoft.EntityFrameworkCore;

namespace PersonFit.Query.Planner.Application.Queries.QueryDispatcher;
using Infrastructure.Postgres;
using PersonFit.Core.Queries;
using Dtos;
internal class GetPlannerQueryHandler : IQueryHandler<GetPlannerQuery, IEnumerable<QueryPlannerDto>>
{
    private readonly PostgresPlannerReadContext _context;

    public GetPlannerQueryHandler(PostgresPlannerReadContext context)
    {
        _context = context;
    }
    public Task<IEnumerable<QueryPlannerDto>> HandleAsync(GetPlannerQuery command, CancellationToken token = default)
    {
        throw new NotImplementedException();
    } 
}