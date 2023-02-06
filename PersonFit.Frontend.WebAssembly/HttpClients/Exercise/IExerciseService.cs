namespace PersonFit.Frontend.WebAssembly.HttpClients.Exercise;

public interface IExerciseService
{
    Task<IEnumerable<ExerciseDto>> Get(CancellationToken token = default);
}

public class FakeExerciseService : IExerciseService
{
    private readonly HttpClient _client;
    private HashSet<ExerciseDto> _dtos = new HashSet<ExerciseDto>();

    public FakeExerciseService(HttpClient client)
    {
        _client = client;
        _dtos.Add(new ExerciseDto(Guid.NewGuid(), "Plank", new[] { "Home", "Crossfit", "Gym" }));
        _dtos.Add(new ExerciseDto(Guid.NewGuid(), "WallBall", new[] { "Home", "Crossfit", "Gym" }));
        _dtos.Add(new ExerciseDto(Guid.NewGuid(), "Burpes", new[] { "Home", "Crossfit", "Gym" }));
        _dtos.Add(new ExerciseDto(Guid.NewGuid(), "SKI ", new[] { "Crossfit", "Gym" }));
    }
    public Task<IEnumerable<ExerciseDto>> Get(CancellationToken token = default)
    {
        return Task.FromResult(_dtos.AsEnumerable());
    }
}