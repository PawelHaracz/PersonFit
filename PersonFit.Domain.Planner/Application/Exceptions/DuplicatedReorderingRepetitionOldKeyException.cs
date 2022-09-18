namespace PersonFit.Domain.Planner.Application.Exceptions;
using PersonFit.Core.Exceptions;

internal class DuplicatedReorderingRepetitionOldKeyException : AppException
{
    public int DuplicatedKey { get; }
    public int NewValue { get; }
    public int CurrentNewValue { get; }
    public override string Code { get; } = "duplicated_reordering_repetition_old_key";

    public DuplicatedReorderingRepetitionOldKeyException(int duplicatedKey, int newValue, int currentNewValue):base($"Trying once more time reorder key {duplicatedKey} to {newValue} but previous version was {currentNewValue}")
    {
        DuplicatedKey = duplicatedKey;
        NewValue = newValue;
        CurrentNewValue = currentNewValue;
    }
}