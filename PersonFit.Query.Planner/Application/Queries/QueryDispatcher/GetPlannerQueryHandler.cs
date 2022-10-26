namespace PersonFit.Query.Planner.Application.Queries.QueryDispatcher;
using PersonFit.Core.Queries;
using Core;
using Dtos;
internal class GetPlannerQueryHandler : IQueryHandler<GetPlannerQuery, IEnumerable<QueryPlannerDto>>
{
    private readonly IReadDbContext _context;
    private readonly string _query = "select * from ";
    public GetPlannerQueryHandler(IReadDbContext context)
    {
        _context = context;
    }
    public Task<IEnumerable<QueryPlannerDto>> HandleAsync(GetPlannerQuery command, CancellationToken token = default)
    {
        throw new NotImplementedException();
        //return  _context.QueryAsync<IEnumerable<QueryPlannerDto>>(_query, command.OwnerId,  token);
    } 
}