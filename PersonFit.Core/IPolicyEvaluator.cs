namespace PersonFit.Core;
using Aggregations;

public interface IPolicyEvaluator<in T> where  T : AggregateRoot
{
    Task<bool> Evaluate(T aggregate, CancellationToken token = default);
}