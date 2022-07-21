namespace PersonFit.Domain.Exercise.Api.Dtos.Commands;

public record AddExerciseCommandDto(string Name, string Description, IEnumerable<string> Tags);