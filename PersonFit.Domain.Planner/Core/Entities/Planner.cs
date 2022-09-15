namespace PersonFit.Domain.Planner.Core.Entities;
using PersonFit.Core.Aggregations;
using Enums;
using Exceptions;
using Events;

internal sealed class Planner: AggregateRoot, IAggregateRoot
{
    public Guid UserId { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public PlannerStatus Status { get; private set; }
    public IEnumerable<Guid> PlannerExercises
    {
        get => _plannerExercises.AsEnumerable();
        private init => _plannerExercises = new HashSet<Guid>(value);
    }

    private readonly ISet<Guid> _plannerExercises;

    public Planner(Guid id,Guid userId, DateTime startTime, DateTime endTime, PlannerStatus status, IEnumerable<Guid> plannerExercises, int version = 0)
    {
        Id = id;
        UserId = userId;
        StartTime = startTime.ToUniversalTime();
        EndTime = endTime.ToUniversalTime();
        PlannerExercises = plannerExercises;
        Status = status;
        Version = version;
    }

    public static Planner Create(Guid id, Guid userId, DateTime startTime, DateTime endTime)
    {
       var status = GetStatusBasedOnTimeRange(userId, startTime, endTime);

       var planner = new Planner(id, userId, startTime, endTime, status, Enumerable.Empty<Guid>());
       planner.AddEvent(new CreatedNewPlannerEvent(id, userId, startTime, endTime, status));

       return planner;
    }

    public void AddExercises(IEnumerable<Guid> plannerExercises)
    {
        foreach (var plannerExercise in plannerExercises)
        {
            var hasExists = _plannerExercises.Contains(plannerExercise);
            if (hasExists)
            {
                continue;
            }

            _plannerExercises.Add(plannerExercise);
            AddEvent(new AddedPlannerExercise(plannerExercise));
        }
    }
    
    public void RemoveExercises(IEnumerable<Guid> plannerExercises)
    {
        foreach (var plannerExercise in plannerExercises)
        {
            var hasExists = _plannerExercises.Contains(plannerExercise);
            if (!hasExists)
            {
                continue;
            }

            _plannerExercises.Remove(plannerExercise);
            AddEvent(new AddedPlannerExercise(plannerExercise));
        }
    }

    private static PlannerStatus GetStatusBasedOnTimeRange(Guid userId, DateTime startTime, DateTime endTime)
    {
        if (startTime < endTime)
        {
            throw new PlannerCreatingException(userId, startTime, endTime);
        }

        if (startTime <= DateTime.UtcNow && endTime >= DateTime.UtcNow)
        {
            return PlannerStatus.Activate;
        }
        if (startTime >= DateTime.UtcNow)
        {
            return PlannerStatus.Pending;
        }

        throw new PlannerCreatingException(userId, startTime, endTime);
    }
}