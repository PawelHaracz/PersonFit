using PersonFit.Domain.Exercise.Core.Enums;

namespace PersonFit.Domain.Exercise.Core.ValueObjects;

internal struct MediaContent: IEquatable<MediaContent>
{
    public string Url { get; }
    public MediaContentType Type { get; }
    public MediaContent(string url, MediaContentType type) => (Url, Type) = (url, type);

    public bool Equals(MediaContent other)
    {
        return Url == other.Url && Type == other.Type;
    }

    public override bool Equals(object obj)
    {
        return obj is MediaContent other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Url, (int)Type);
    }
}