namespace PersonFit.Query.Planner.Application.Dtos;
using System.Text.Json.Serialization;
using PersonFit.Infrastructure;
public record PlannersDto(Guid Id,string Status, [property: JsonConverter(typeof(DateOnlyJsonConverter))]DateOnly StartTime, [property: JsonConverter(typeof(DateOnlyJsonConverter))]DateOnly EndTime)
{
    internal static IEnumerable<PlannersDto> Map(IEnumerable<QueryPlannerDto> dto)
    {
        return dto.Select(d => new PlannersDto(d.Id, d.Status.ToString(), DateOnly.FromDateTime(d.StartTime),
            DateOnly.FromDateTime(d.EndTime)));
    }
}