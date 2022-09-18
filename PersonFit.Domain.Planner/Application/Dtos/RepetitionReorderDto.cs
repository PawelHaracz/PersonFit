namespace PersonFit.Domain.Planner.Application.Dtos;

internal  readonly struct RepetitionReorderDto : IEquatable<RepetitionReorderDto>
{
    public int Old { get; init; }
    public int New { get; init; }

    public bool Equals(RepetitionReorderDto other)
    {
        return Old == other.Old && New == other.New;
    }

    public override bool Equals(object obj)
    {
        return obj is RepetitionReorderDto other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Old, New);
    }
}