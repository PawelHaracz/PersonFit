namespace PersonFit.Core.Queries;

public interface IFilter<TResult, in TQuery> where TQuery : IQuery
{
    IEnumerable<TResult> Filter(IEnumerable<TResult> values, TQuery query);
}