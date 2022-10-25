
namespace PersonFit.Domain.Planner.Application.Queries;
using PersonFit.Core.Queries;
using Dtos;
using Enums;
using PersonFit.Core.Commands;

internal record GetPlannerQuery(Guid OwnerId, PlannerStatus Status): IQuery<IEnumerable<QueryPlannerDto>>;