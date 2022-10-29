using PersonFit.Query.Planner.Application.Daos;

namespace PersonFit.Query.Planner.Application.Queries.QueryDispatcher;
using PersonFit.Core.Queries;
using Core;
using Dtos;
using Microsoft.Extensions.Logging;
internal class GetPlannerQueryHandler : IQueryHandler<GetPlannerQuery, IEnumerable<PlannersDto>>
{
    private readonly IReadDbContext _context;
    private readonly IDaoToDtoMapper<IEnumerable<QueryPlannerDao>, IEnumerable<PlannersDto>> _mapper;
    private readonly ILogger<GetPlannerQueryHandler> _logger;

    public GetPlannerQueryHandler(IReadDbContext context,IDaoToDtoMapper<IEnumerable<QueryPlannerDao>, IEnumerable<PlannersDto>> mapper,  ILogger<GetPlannerQueryHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<IEnumerable<PlannersDto>> HandleAsync(GetPlannerQuery command, CancellationToken token = default)
    {
        if (command.OwnerId == Guid.Empty)
        {
            _logger.LogWarning("Empty ownerId handled in {querHandler}", nameof(GetPlannerQueryHandler));
            return ArraySegment<PlannersDto>.Empty;
        }
        
        var results = await  _context.QueryAsync<QueryPlannerDao>(
            QueryPlannerDao.GetSqlQuery(), 
            QueryPlannerDao.GetParameters(command.OwnerId, command.Status),
            null, 
            token);
    
        return _mapper.Map(results);
    } 
}

