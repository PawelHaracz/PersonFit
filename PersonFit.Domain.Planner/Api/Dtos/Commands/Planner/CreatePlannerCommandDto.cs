using System.Text.Json.Serialization;
using PersonFit.Infrastructure;

namespace PersonFit.Domain.Planner.Api.Dtos.Commands.Planner;

public record CreatePlannerCommandDto( [property: JsonConverter(typeof(DateOnlyJsonConverter))] DateOnly StartTime, [property: JsonConverter(typeof(DateOnlyJsonConverter))] DateOnly EndTime)
{
}