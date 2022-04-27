using PersonFit.Core;

namespace PersonFit.Domain.Exercise.Application.Commands;

public record AddExerciseCommand(string Name, string Description, IEnumerable<string> Tags) : ICommand
{
}