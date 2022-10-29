namespace PersonFit.Domain.Planner.Core.ValueObjects;
using PersonFit.Core.Enums;

internal struct DailyPlanner : IEquatable<DailyPlanner>
{
    public DailyPlanner(DayOfWeek dayOfWeek, TimeOfDay timeOfDay, IEnumerable<Guid> exercisesPlanner)
    {
        DayOfWeek = dayOfWeek;
        TimeOfDay = timeOfDay;
        _exercisesPlanner = new HashSet<Guid>(exercisesPlanner);
    }
    
    public static DailyPlanner Default => new(DayOfWeek.Monday, TimeOfDay.WholeDay, new[] { Guid.Empty });

    public IEnumerable<Guid> ExercisesPlanner => _exercisesPlanner.AsEnumerable();

    public DayOfWeek DayOfWeek { get; }
    public TimeOfDay TimeOfDay { get; }

    private readonly ISet<Guid> _exercisesPlanner;
    
    public bool Equals(DailyPlanner other)
    {
        return _exercisesPlanner.SetEquals(other._exercisesPlanner) && DayOfWeek == other.DayOfWeek && TimeOfDay == other.TimeOfDay;
    }

    public override bool Equals(object obj)
    {
        return obj is DailyPlanner other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_exercisesPlanner, (int)DayOfWeek, (int)TimeOfDay);
    }

    public void UpdateExercisePlanners(IEnumerable<Guid> newPlanners)
    {
        _exercisesPlanner.Clear();
        foreach (var planner in newPlanners)
        {
            _exercisesPlanner.Add(planner);
        }
    }


}