namespace PersonFit.Frontend.WebAssembly.HttpClients.Exercise;

public record ExerciseDto(Guid Id, string Name, IEnumerable<string> Tags);