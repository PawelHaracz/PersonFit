namespace PersonFit.Frontend.WebAssembly.Infrastructure.Notifications;

public record ConnectionStateChanged(ConnectionState State, string? Message) : INotificationMessage;