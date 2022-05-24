namespace PersonFit.Domain.Exercise.Api.Dtos;

public record AddExerciseCommandDto(string Name, string Description, IEnumerable<string> Tags);