namespace PersonFit.Query.Planner.Application.Dtos;

public struct Repetition
{
    public int Order { get; set; }
    public int Count { get; set; }
    public string MeasurementUnit { get; set; }
    public string Note { get; set; }
}