using PersonFit.Core.Tests.Extensions;

namespace PersonFit.Domain.Planner.Tests.Extensions;

internal static class AggregateRootExtensions
{
    public static bool Compare(this Core.Entities.PlannerExercise first, Core.Entities.PlannerExercise second)
    {
        return first.Id == second.Id &&
               first.OwnerId == second.OwnerId &&
               first.ExerciseId == second.ExerciseId &&
               first.Repetitions.CompareArrays(second.Repetitions);
    }
}