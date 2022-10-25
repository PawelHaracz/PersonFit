namespace PersonFit.Domain.Exercise.Infrastructure.Postgres.Documents;
using System.ComponentModel.DataAnnotations.Schema;
using PersonFit.Core.Aggregations;

internal class ExerciseDocument: IIdentifiable<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    [Column(TypeName = "jsonb")]
    public IEnumerable<string> Tags { get; set; }
    [Column(TypeName = "jsonb")]
    public IEnumerable<Media> Media { get; set; }
    public int Version { get; set; }
    
}