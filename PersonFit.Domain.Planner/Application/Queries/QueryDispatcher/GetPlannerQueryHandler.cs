namespace PersonFit.Domain.Planner.Application.Queries.QueryDispatcher;
using PersonFit.Core.Queries;
using Dtos;


internal class GetPlannerQueryHandler : IQueryHandler<GetPlannerQuery, IEnumerable<QueryPlannerDto>>
{
    public Task<IEnumerable<QueryPlannerDto>> HandleAsync(GetPlannerQuery command, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}