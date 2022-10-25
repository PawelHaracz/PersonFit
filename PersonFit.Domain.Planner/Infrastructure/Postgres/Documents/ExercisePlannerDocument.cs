
namespace PersonFit.Domain.Planner.Infrastructure.Postgres.Documents;
using System.ComponentModel.DataAnnotations.Schema;
using PersonFit.Core.Aggregations;

internal class ExercisePlannerDocument : IIdentifiable<Guid>
{
    public Guid Id { get; set; }
    public int Version { get; set; }
    public Guid ExerciseId { get; set; }
    public Guid OwnerId { get; set; }
    
    [Column(TypeName = "jsonb")]
    public IEnumerable<RepetitionDao> Repetitions { get; set; }
}