namespace PersonFit.Frontend.WebAssembly.HttpClients.Exercise;

public record ExerciseSummaryDto(Guid Id, string Name, IEnumerable<string> Tags,
    IEnumerable<ExerciseSummaryContent> Contents)
{
    public static ExerciseSummaryDto Default =
        new ExerciseSummaryDto(Guid.Empty, String.Empty, ArraySegment<string>.Empty, ArraySegment<ExerciseSummaryContent>.Empty);
}
public record ExerciseSummaryContent(string Url, string Type){}
