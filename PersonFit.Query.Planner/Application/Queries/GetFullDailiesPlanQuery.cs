using PersonFit.Query.Planner.Application.Daos;

namespace PersonFit.Query.Planner.Application.Queries;
using PersonFit.Core.Queries;
using Dtos;
using Enums;

public record GetFullDailiesPlanQuery(Guid OwnerId, Guid PlannerId): IQuery<QueryFullDailiesPlannerDao>, IQuery<FullDailiesPlannerDto>;


