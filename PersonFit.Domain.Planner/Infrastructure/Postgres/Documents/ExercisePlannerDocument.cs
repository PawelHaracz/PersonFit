namespace PersonFit.Domain.Planner.Infrastructure.Postgres.Documents;
using PersonFit.Core.Aggregations;

internal class ExercisePlannerDocument : IIdentifiable<Guid>
{
    public Guid Id { get; set; }
    public int Version { get; set; }
}