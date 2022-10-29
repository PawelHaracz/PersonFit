namespace PersonFit.Query.Planner.Application.Dtos;
using Exceptions;
using Enums;
using PersonFit.Core.Enums;
using System.Text.Json.Serialization;
using PersonFit.Infrastructure;
public record FullDailiesPlannerDto(Guid Id, [property: JsonConverter(typeof(DateOnlyJsonConverter))]DateOnly StartTime, [property: JsonConverter(typeof(DateOnlyJsonConverter))]DateOnly EndTime, PlannerStatus Status,
    IEnumerable<DailyPlan> DailyPlans)
{
    internal static FullDailiesPlannerDto Map(IEnumerable<QueryFullDailesPlannerDto> dto)
    {
        try
        {
            var dailies = from d in dto
                group d by new
                {
                    d.DailyDayOfWork,
                    d.DailyTimeOfWork,
                    d.PlanId,
                }
                into dp
                select new DailyPlan(
                    dp.Key.DailyDayOfWork,
                    dp.Key.DailyTimeOfWork,
                    dp.Select(ex => new Exercise(ex.ExerciseName, ex.ExerciseDescription, ex.ExerciseRepetitions.Value, ex.ExerciseTags.Value, ex.ExerciseMedia.Value))
                );

            var planners = dto.GroupBy(d => new
            {
                d.PlanId,
                d.PlannerStatus,
                d.PlannerStartTime,
                d.PlannerEndTime
            }).Select(p => p.Key).ToArray();

            if (planners.Count() != 1)
            {
                throw new Exception();
            }

            var planner = planners.First();
            return new FullDailiesPlannerDto(planner.PlanId, DateOnly.FromDateTime(planner.PlannerStartTime), DateOnly.FromDateTime(planner.PlannerEndTime), planner.PlannerStatus, dailies);
        }
        catch (Exception e)
        {
            throw new MappingObjectException(nameof(FullDailiesPlannerDto));
        }
    }
};

public record DailyPlan(DayOfWeek DayOfWeek, TimeOfDay TimeOfDay, IEnumerable<Exercise> Exercises);

public record Exercise(string Name, string Description, IEnumerable<Repetition> Repetitions, IEnumerable<string> Tags, IEnumerable<string> Media);