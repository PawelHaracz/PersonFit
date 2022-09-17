using PersonFit.Core.Aggregations;

namespace PersonFit.Domain.Planner.Infrastructure.Postgres.Documents;

internal class ExercisePlannerDocument : IIdentifiable<Guid>
{
    public Guid Id { get; set; }
    public int Version { get; set; }
}