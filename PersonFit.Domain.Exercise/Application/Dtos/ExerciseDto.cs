namespace PersonFit.Domain.Exercise.Application.Dtos;

public record ExerciseDto(Guid Id, string Name, IEnumerable<string> Tags);