using System.Net.Http.Json;

namespace PersonFit.Frontend.WebAssembly.HttpClients.Exercise;

public class ExerciseService : IExerciseService
{
    private readonly HttpClient _httpClient;
    public ExerciseService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<IEnumerable<ExerciseDto>> Get(CancellationToken token = default)
    {
        var items = await _httpClient.GetFromJsonAsync<IEnumerable<ExerciseDto>>("/exercise", token);

        return items ?? Enumerable.Empty<ExerciseDto>();
    }
}