namespace PersonFit.Domain.Planner.Infrastructure.Postgres.Documents;
using PersonFit.Core.Aggregations;
using System.ComponentModel.DataAnnotations.Schema;

internal class PlannerDocument: IIdentifiable<Guid>
{
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int Status { get; set; }
    public int Version { get; set; }
    [Column(TypeName = "jsonb")]
    public IEnumerable<DailyPlannerDao> DailyPlanners { get; set; }
}