using MediatR;

namespace RYB.MediatR.Notifications;

public record UserCreated(string UserId) : INotification;

