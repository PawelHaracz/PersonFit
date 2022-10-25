using PersonFit.Core.Exceptions;

namespace PersonFit.Domain.Planner.Core.Exceptions;

public class WrongOrderingMappingCollectionException : DomainException
{
    public override string Code { get; } = "wrong_ordering_mapping_collection";
    
    public Guid Id { get; }
    public IDictionary<int, int> OrderingMapping { get; } 

    public WrongOrderingMappingCollectionException(Guid id, IDictionary<int, int> orderingMapping) : base("reordering collection contains duplicates for exercise planner {}")
    {
        Id = id;
        OrderingMapping = orderingMapping;
    }
}