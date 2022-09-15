namespace PersonFit.Domain.Planner.Core.Entities;
using PersonFit.Core.Aggregations;
using Enums;

internal sealed class Planner: AggregateRoot, IAggregateRoot
{
    public Guid UserId { get; private set; }
    public TimeSpan StartTime { get; private set; }
    public TimeSpan EndTime { get; private set; }
    public PlannerStatus Status { get; private set; }
    public IEnumerable<Guid> PlannerExercises
    {
        get => _plannerExercises;
        private init => _plannerExercises = new HashSet<Guid>(value);
    }

    private readonly ISet<Guid> _plannerExercises;
}