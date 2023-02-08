namespace PersonFit.Frontend.WebAssembly.HttpClients.Exercise;

public interface IExerciseService
{
    Task<IEnumerable<ExerciseDto>> Get(CancellationToken token = default);
    Task<ExerciseSummaryDto> Get(Guid id, CancellationToken token = default);
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

    public Task<ExerciseSummaryDto> Get(Guid id, CancellationToken token = default)
    {
        var dto = _dtos.Single(d => d.Id == id);
        var summary = new ExerciseSummaryDto(id, dto.Name, dto.Tags, new[]
        {
            new ExerciseSummaryContent("http://techslides.com/demos/sample-videos/small.mp4", "video"),
            new ExerciseSummaryContent("https://www.qries.com/images/banner_logo.png", "image"),
        });
        return Task.FromResult(summary);
    }
}

