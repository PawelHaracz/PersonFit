namespace PersonFit.Core.Tests.Extensions;
using System.Collections.Generic;
using System.Linq;

public static class IEnumerableExtensions
{
    public static bool CompareArrays<T>(this IEnumerable<T> first, IEnumerable<T> second)
    {
        if (!first.Any() && !second.Any())
        {
            return true;
        }

        return first.SequenceEqual(second);
    }
}