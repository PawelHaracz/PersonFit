using PersonFit.Core;
using PersonFit.Core.Commands;

namespace PersonFit.Domain.Exercise.Application.Commands;

public record AddExerciseCommand(Guid Id, string Name, string Description, IEnumerable<string> Tags) : ICommand
{
}