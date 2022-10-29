namespace PersonFit.Query.Planner.Application.Queries.QueryDispatcher;
using Core;
using Daos;
using Dtos;
using PersonFit.Core.Queries;

internal class GetFullDailiesPlanQueryHandler: IQueryHandler<GetFullDailiesPlanQuery, FullDailiesPlannerDto>
{    
    private readonly IReadDbContext _context;
    private readonly IDaoToDtoMapper<IEnumerable<QueryFullDailiesPlannerDao>, FullDailiesPlannerDto> _mapper;

    public GetFullDailiesPlanQueryHandler(IReadDbContext context, IDaoToDtoMapper<IEnumerable<QueryFullDailiesPlannerDao>, FullDailiesPlannerDto> mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<FullDailiesPlannerDto> HandleAsync(GetFullDailiesPlanQuery query, CancellationToken token = default)
    {
        var results = await  _context.QueryAsync<QueryFullDailiesPlannerDao>(
            QueryFullDailiesPlannerDao.GetSqlQuery(), 
            QueryFullDailiesPlannerDao.CreateParameters(query.OwnerId, query.PlannerId), 
            null,  
            token);
        
        return _mapper.Map(results);
    }
}