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

    public async Task<ExerciseSummaryDto> Get(Guid id, CancellationToken token = default)
    {
        var item = await _httpClient.GetFromJsonAsync<ExerciseSummaryDto>($"/exercise/{id}", token);
        if (item != null) return item;

        return ExerciseSummaryDto.Default;
    }
}