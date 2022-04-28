using PersonFit.Core;

namespace PersonFit.Domain.Exercise.Application.Commands;

public record AddExerciseCommand(Guid Id, string Name, string Description, IEnumerable<string> Tags) : ICommand
{
}