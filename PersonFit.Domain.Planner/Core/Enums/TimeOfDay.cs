namespace PersonFit.Domain.Planner.Core.Enums;

[Flags]
internal enum TimeOfDay
{
    Morning = 1,
    Afternoon = 2,
    Evening = 4,
    Night = 8,
    WholeDay = Morning | Afternoon |Evening | Night
}