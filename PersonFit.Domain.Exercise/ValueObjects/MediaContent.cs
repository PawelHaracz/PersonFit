namespace PersonFit.Domain.Exercise.ValueObjects;

internal struct MediaContent
{
    public string Url { get; }

    public MediaContent(string url) => (Url) = (url);

}