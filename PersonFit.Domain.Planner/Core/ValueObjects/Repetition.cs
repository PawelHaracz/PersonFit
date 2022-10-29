namespace PersonFit.Domain.Planner.Core.ValueObjects;
using PersonFit.Core.Enums;
internal struct Repetition: IEquatable<Repetition>, IComparable<Repetition>, IComparable
{
    public Repetition(int order, int count, MeasurementUnit unit, string note)
    {
        Order = order;
        Count = count;
        Unit = unit;
        Note = note;
    }

    public int Order { get; private set; }
    public int Count { get; }
    public string Note { get; }
    public MeasurementUnit Unit { get; }

    public static Repetition Default => new (0, 0, MeasurementUnit.None, string.Empty);

    public bool Equals(Repetition other)
    {
        return Order == other.Order && Count == other.Count && Unit == other.Unit && Note.Equals(other.Note);
    }

    public override bool Equals(object obj)
    {
        return obj is Repetition other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Order, Count, (int)Unit, Note);
    }
    
    public void ChangeOrder(int newOrder)
    {
        Order = newOrder;
    }

    public int CompareTo(object obj)
    {
        if (obj is Repetition repetition)
        {
            return CompareTo(repetition);
        }

        return -1;
    }

    
    public int CompareTo(Repetition other)
    {
        var orderComparison = Order.CompareTo(other.Order);
        if (orderComparison != 0) return orderComparison;
        var countComparison = Count.CompareTo(other.Count);
        if (countComparison != 0) return countComparison;
        var noteComparison = string.Compare(Note, other.Note, StringComparison.Ordinal);
        if (noteComparison != 0) return noteComparison;
        return Unit.CompareTo(other.Unit);
    }
}