namespace PersonFit.Domain.Planner.Infrastructure.Postgres.Documents;
using Core.Enums;
using Core.ValueObjects;

internal static class ExercisePlannerExtensions
{
    public static Core.Entities.PlannerExercise AsEntity(this ExercisePlannerDocument document)
        => new(document.Id,
            document.OwnerId,
            document.ExerciseId,
            document.Repetitions.Select(r =>
                new Repetition(r.Order, r.Count, (MeasurementUnit)r.MeasurementUnit, r.Note)), 
            document.Version);

    public static ExercisePlannerDocument AsDocument(this Core.Entities.PlannerExercise entity)
        => new()
        {
            Id = entity.Id,
            ExerciseId = entity.ExerciseId,
            OwnerId = entity.OwnerId,
            Repetitions = entity.Repetitions.Select(r => new RepetitionDao()
            {
                Count = r.Count,
                Note = r.Note,
                Order = r.Order,
                MeasurementUnit = (int)r.Unit
            }),
            Version = entity.Version
        };
}