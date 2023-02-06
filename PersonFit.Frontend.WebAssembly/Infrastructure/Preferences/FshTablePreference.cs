using PersonFit.Frontend.WebAssembly.Infrastructure.Notifications;

namespace PersonFit.Frontend.WebAssembly.Infrastructure.Preferences;

public class FshTablePreference : INotificationMessage
{
    public bool IsDense { get; set; } = true;
    public bool IsStriped { get; set; } = true;
    public bool HasBorder { get; set; } = true;
    public bool IsHoverable { get; set; } = true;
}