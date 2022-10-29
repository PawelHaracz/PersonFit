namespace PersonFit.Query.Planner.Infrastructure.Mappers;
using Core.Queries;
using Application.Daos;
using Application.Dtos;
using Application.Exceptions;

internal class FullDailiesPlannerMapper : IDaoToDtoMapper<IEnumerable<QueryFullDailiesPlannerDao>, FullDailiesPlannerDto>
{
    public FullDailiesPlannerDto Map(IEnumerable<QueryFullDailiesPlannerDao> dao)
    {
        try
        {
            var dailies = from d in dao
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

            var planners = dao.GroupBy(d => new
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
}