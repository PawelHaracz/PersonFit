namespace PersonFit.Domain.Planner.Infrastructure.Postgres.Documents;

internal class DailyPlannerDao
{
    public int DayOfWeek { get; set; }
    public int TimeOfDay { get; set; }
    public IEnumerable<Guid> ExercisesPlanners { get; set; }
}