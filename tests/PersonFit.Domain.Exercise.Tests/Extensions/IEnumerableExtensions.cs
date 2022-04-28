using System.Collections.Generic;
using System.Linq;

namespace PersonFit.Domain.Exercise.Tests.Extensions;

public static class IEnumerableExtensions
{
    public static bool CompareArrays<T>(this IEnumerable<T> first, IEnumerable<T> second)
    {
        if (!first.Any() && !second.Any())
        {
            return true;
        }

        return first.OrderBy(i => i).SequenceEqual(second.OrderBy(i => i));
    }
}