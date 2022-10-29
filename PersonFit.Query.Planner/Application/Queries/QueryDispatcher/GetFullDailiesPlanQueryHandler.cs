namespace PersonFit.Query.Planner.Application.Queries.QueryDispatcher;
using Core;
using Dtos;
using PersonFit.Core.Queries;

internal class GetFullDailiesPlanQueryHandler: IQueryHandler<GetFullDailiesPlanQuery, FullDailiesPlannerDto>
{    
    private readonly IReadDbContext _context;
    public GetFullDailiesPlanQueryHandler(IReadDbContext context)
    {
        _context = context;
    }
    
    public async Task<FullDailiesPlannerDto> HandleAsync(GetFullDailiesPlanQuery query, CancellationToken token = default)
    {
        var results = await  _context.QueryAsync<QueryFullDailesPlannerDto>(
            QueryFullDailesPlannerDto.GetSqlQuery(), 
            QueryFullDailesPlannerDto.CreateParameters(query.OwnerId, query.PlannerId), 
            null,  
            token);
        
        return FullDailiesPlannerDto.Map(results);
    }
}