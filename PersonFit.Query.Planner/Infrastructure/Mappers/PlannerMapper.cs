namespace PersonFit.Query.Planner.Infrastructure.Mappers;
using Core.Queries;
using Application.Daos;
using Application.Dtos;
using Application.Exceptions;
internal class PlannerMapper : IDaoToDtoMapper<IEnumerable<QueryPlannerDao>, IEnumerable<PlannersDto>>
{
    public IEnumerable<PlannersDto> Map(IEnumerable<QueryPlannerDao> dao)
    {
        return dao.Select(d =>
        {
            if (d.Id == Guid.Empty || d.StartTime == default || d.EndTime == default)
            {
                throw new MappingObjectException(nameof(PlannerMapper));
            }

            if (d.StartTime > d.EndTime)
            {
                throw new MappingObjectException(nameof(PlannerMapper));
            }
            
            return new PlannersDto(d.Id, d.Status.ToString(), DateOnly.FromDateTime(d.StartTime),
                DateOnly.FromDateTime(d.EndTime));
        });
    }
}