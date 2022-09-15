namespace PersonFit.Domain.Planner.Core.ValueObjects;
using Enums;
internal struct Repetition: IEquatable<Repetition>
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
}