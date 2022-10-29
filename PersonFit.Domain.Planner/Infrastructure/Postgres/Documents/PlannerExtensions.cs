namespace PersonFit.Domain.Planner.Infrastructure.Postgres.Documents;
using Core.Enums;
using Core.ValueObjects;
using PersonFit.Core.Enums;
internal static class  PlannerExtensions
{
    public static Core.Entities.Planner AsEntity(this PlannerDocument document)
        => new(document.Id,
            document.OwnerId,
            document.StartTime,
            document.EndTime,
            (PlannerStatus)document.Status,
            document.DailyPlanners.Select(r =>
                new DailyPlanner((DayOfWeek)r.DayOfWeek, (TimeOfDay)r.TimeOfDay, r.ExercisesPlanners.ToArray())), 
            document.Version);

    public static PlannerDocument AsDocument(this Core.Entities.Planner entity)
        => new()
        {
            Id = entity.Id,
            Version = entity.Version,
            OwnerId = entity.OwnerId,
            Status = (int)entity.Status,
            EndTime = entity.EndTime,
            StartTime = entity.StartTime,
            DailyPlanners = entity.DailyPlanners.Select(d =>
            new DailyPlannerDao()
            { 
                DayOfWeek = (int)d.DayOfWeek,
                TimeOfDay = (int)d.TimeOfDay,
                ExercisesPlanners = d.ExercisesPlanner.ToArray()
            })
        };
}