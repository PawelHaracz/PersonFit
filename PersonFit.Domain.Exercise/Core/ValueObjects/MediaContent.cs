namespace PersonFit.Domain.Exercise.Core.ValueObjects;
using Enums;

internal readonly struct MediaContent: IEquatable<MediaContent>
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

    public int CompareTo(MediaContent other)
    {
        var urlComparison = string.Compare(Url, other.Url, StringComparison.Ordinal);
        if (urlComparison != 0) return urlComparison;
        return Type.CompareTo(other.Type);
    }
}