namespace PersonFit.Domain.Planner.Infrastructure.Postgres.Documents;

internal class RepetitionDao
{
    public int Order { get; set; }
    public int MeasurementUnit { get; set; }
    public int Count { get; set; }
    public string Note { get; set; }
}