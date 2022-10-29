namespace PersonFit.Query.Planner.Application.Queries.QueryDispatcher;
using PersonFit.Core.Queries;
using Core;
using Dtos;
using Microsoft.Extensions.Logging;
internal class GetPlannerQueryHandler : IQueryHandler<GetPlannerQuery, IEnumerable<QueryPlannerDto>>
{
    private readonly IReadDbContext _context;
    private readonly ILogger<GetPlannerQueryHandler> _logger;

    public GetPlannerQueryHandler(IReadDbContext context, ILogger<GetPlannerQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }
    public async Task<IEnumerable<QueryPlannerDto>> HandleAsync(GetPlannerQuery command, CancellationToken token = default)
    {
        if (command.OwnerId == Guid.Empty)
        {
            _logger.LogWarning("Empty ownerId handled in {querHandler}", nameof(GetPlannerQueryHandler));
            return ArraySegment<QueryPlannerDto>.Empty;
        }
        
        var results = await  _context.QueryAsync<QueryPlannerDto>(
            QueryPlannerDto.GetSqlQuery(), 
            QueryPlannerDto.GetParameters(command.OwnerId, command.Status),
            null, 
            token);

        return results;
    } 
}

