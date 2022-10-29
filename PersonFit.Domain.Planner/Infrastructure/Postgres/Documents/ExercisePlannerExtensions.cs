namespace PersonFit.Domain.Planner.Infrastructure.Postgres.Documents;
using Core.ValueObjects;
using PersonFit.Core.Enums;
internal static class ExercisePlannerExtensions
{
    public static Core.Entities.PlannerExercise AsEntity(this ExercisePlannerDocument document)
        => new(document.Id,
            document.OwnerId,
            document.ExerciseId,
            document.Repetitions.Select(r =>
                new Repetition(r.Order, r.Count, Enum.Parse<MeasurementUnit>(r.MeasurementUnit), r.Note)), //MeasurementUnit
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
                MeasurementUnit = r.Unit.ToString()
            }),
            Version = entity.Version
        };
}