namespace PersonFit.Query.Planner.Application.Queries;
using PersonFit.Core.Queries;
using Dtos;
using Enums;

public record GetFullDailiesPlanQuery(Guid OwnerId, PlannerStatus Status): IQuery<QueryFulldailesPlannerDto>, IQuery<IEnumerable<QueryFulldailesPlannerDto>>;


