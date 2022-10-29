
using PersonFit.Core.Queries;
using PersonFit.Query.Planner.Application.Daos;
using PersonFit.Query.Planner.Application.Dtos;
using PersonFit.Query.Planner.Application.Enums;

namespace PersonFit.Query.Planner.Application.Queries;

internal record GetPlannerQuery(Guid OwnerId, PlannerStatus Status): IQuery<IEnumerable<PlannersDto>>;