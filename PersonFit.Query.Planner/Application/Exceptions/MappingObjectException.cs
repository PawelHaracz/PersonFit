using PersonFit.Core.Exceptions;

namespace PersonFit.Query.Planner.Application.Exceptions;

internal class MappingObjectException: DomainException
{
    public override string Code { get; } = "cannot_map_query_to_dto";

    public MappingObjectException(string className) : base($"cannot proper map object in dto {className} ")
    {
    }
}