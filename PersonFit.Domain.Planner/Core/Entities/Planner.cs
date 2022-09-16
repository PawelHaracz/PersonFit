namespace PersonFit.Domain.Planner.Core.Entities;
using PersonFit.Core.Aggregations;
using Enums;
using Exceptions;
using Events;
using ValueObjects;

internal sealed class Planner: AggregateRoot, IAggregateRoot
{
    public Guid UserId { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public PlannerStatus Status { get; private set; }
    public IEnumerable<DailyPlanner> DailyPlanners
    {
        get => _dailyPlanners.AsEnumerable();
        private init => _dailyPlanners = new HashSet<DailyPlanner>(value);
    }

    private readonly ISet<DailyPlanner> _dailyPlanners;

    public Planner(Guid id,Guid userId, DateTime startTime, DateTime endTime, PlannerStatus status, IEnumerable<DailyPlanner> dailyPlanners, int version = 0)
    {
        Id = id;
        UserId = userId;
        StartTime = startTime.ToUniversalTime();
        EndTime = endTime.ToUniversalTime();
        DailyPlanners = dailyPlanners;
        Status = status;
        Version = version;
    }

    public static Planner Create(Guid id, Guid userId, DateTime startTime, DateTime endTime)
    {
        if (userId == default)
        {
            throw new PlannerEmptyUserException();
        } 
        var status = GetStatusBasedOnTimeRange(userId, startTime, endTime);

       var planner = new Planner(id, userId, startTime.ToUniversalTime(), endTime.ToUniversalTime(), status, Enumerable.Empty<DailyPlanner>());
       planner.AddEvent(new CreatedNewPlannerEvent(id, userId, startTime.ToUniversalTime(), endTime.ToUniversalTime(), status));

       return planner;
    }

    public void AddExercises(DayOfWeek dayOfWeek, TimeOfDay timeOfDay, IEnumerable<Guid> plannerExercises)
    {
        var item = _dailyPlanners.SingleOrDefault(p => p.DayOfWeek == dayOfWeek && p.TimeOfDay == timeOfDay, DailyPlanner.Default);
        if (item.Equals(DailyPlanner.Default))
        {
            item = new DailyPlanner(dayOfWeek, timeOfDay, ArraySegment<Guid>.Empty);
        }
        else
        {
            _dailyPlanners.Remove(item);    
        }
        
        var newPlanners = item.ExercisesPlanner.ToList();
        
        foreach (var plannerExercise in plannerExercises)
        {
            var hasExists = newPlanners.Contains(plannerExercise) || plannerExercise == Guid.Empty;
            if (hasExists)
            {
                continue;
            }

            newPlanners.Add(plannerExercise);
            AddEvent(new AddedPlannerExercise(dayOfWeek, timeOfDay, plannerExercise));
        }

        item.UpdateExercisePlanners(newPlanners);
        _dailyPlanners.Add(item);
    }
    
    public void RemoveExercises(DayOfWeek dayOfWeek, TimeOfDay timeOfDay, IEnumerable<Guid> plannerExercises)
    {
        var item = _dailyPlanners.SingleOrDefault(p => p.DayOfWeek == dayOfWeek && p.TimeOfDay == timeOfDay, DailyPlanner.Default);
        if (item.Equals(DailyPlanner.Default))
        {
            return;
        }
        _dailyPlanners.Remove(item);
        var newPlanners = item.ExercisesPlanner.ToList();
        foreach (var plannerExercise in plannerExercises)
        {
            var hasExists = item.ExercisesPlanner.Contains(plannerExercise);
            if (!hasExists)
            {
                continue;
            }

            newPlanners.Add(plannerExercise);
            AddEvent(new RemovedPlannerExercise(dayOfWeek, timeOfDay, plannerExercise));
        }
        item.UpdateExercisePlanners(newPlanners);
        _dailyPlanners.Add(item);
    }

    private static PlannerStatus GetStatusBasedOnTimeRange(Guid userId, DateTime startTime, DateTime endTime)
    {
        if (startTime > endTime)
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