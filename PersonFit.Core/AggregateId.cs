using PersonFit.Core.Exceptions;

namespace PersonFit.Core;

public struct AggregateId : IEquatable<AggregateId>
{
    public Guid Value { get; }

    public AggregateId() : this(Guid.NewGuid())
    {
    }

    public AggregateId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidAggregateIdException(value);
        }

        Value = value;
    }

    public bool Equals(AggregateId other)
    {
        return Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return obj is AggregateId id && Equals(id);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public static implicit operator Guid(AggregateId id)
        => id.Value;

    public static implicit operator AggregateId(Guid id)
        => new AggregateId(id);

    public static bool operator ==(AggregateId first, AggregateId second) => first.Equals(second);

    public static bool operator !=(AggregateId first, AggregateId second) => !(first == second);

    public override string ToString() => Value.ToString();
}