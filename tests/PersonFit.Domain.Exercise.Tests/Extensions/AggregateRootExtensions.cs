namespace PersonFit.Domain.Exercise.Tests.Extensions;

internal static class AggregateRootExtensions
{
    public static bool Compare(this Core.Entities.Exercise first, Core.Entities.Exercise second)
    {
        return first.Id == second.Id && 
               first.Name == second.Name &&
               first.Description == second.Description &&
               first.Contents.CompareArrays(second.Contents) &&
               first.Tags.CompareArrays(second.Tags);
    }
}