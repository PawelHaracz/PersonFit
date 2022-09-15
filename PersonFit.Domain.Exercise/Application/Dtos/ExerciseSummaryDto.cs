namespace PersonFit.Domain.Exercise.Application.Dtos;

public record ExerciseSummaryDto(Guid Id, string Name, IEnumerable<string> Tags, IEnumerable<ExerciseSummaryContent> Contents)
{
    public static ExerciseSummaryDto Default =
        new ExerciseSummaryDto(Guid.Empty, String.Empty, ArraySegment<string>.Empty, ArraySegment<ExerciseSummaryContent>.Empty);
}

public struct ExerciseSummaryContent
{
    public string Url { get; }
    public string Type { get; }

    public ExerciseSummaryContent(string url, string type)
    {
        Url = url;
        Type = type;
    }
}