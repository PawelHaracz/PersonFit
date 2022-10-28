using PersonFit.Query.Planner.Application.Enums;

namespace PersonFit.Query.Planner.Application.Dtos;

internal class QueryFulldailesPlannerDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public IEnumerable<string> Media { get; set; }
    public IEnumerable<string> Tags { get; set; }
    public IEnumerable<Repetition> Repetitions { get; set; }

}

internal struct Repetition
{
    public int Order { get; set; }
    public int Count { get; set; }
    public string MeasurementUnit { get; set; }
    public string Note { get; set; }
}