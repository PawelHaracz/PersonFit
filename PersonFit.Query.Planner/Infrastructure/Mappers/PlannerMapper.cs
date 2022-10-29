using PersonFit.Core.Queries;
using PersonFit.Query.Planner.Application.Daos;
using PersonFit.Query.Planner.Application.Dtos;

namespace PersonFit.Query.Planner.Infrastructure.Mappers;

internal class PlannerMapper : IDaoToDtoMapper<IEnumerable<QueryPlannerDao>, IEnumerable<PlannersDto>>
{
    public IEnumerable<PlannersDto> Map(IEnumerable<QueryPlannerDao> dao)
    {
        return dao.Select(d => new PlannersDto(d.Id, d.Status.ToString(), DateOnly.FromDateTime(d.StartTime),
            DateOnly.FromDateTime(d.EndTime)));
    }
}