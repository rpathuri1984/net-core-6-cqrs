using MediatR;
using RYB.MediatR.Notifications;

namespace RYB.MediatR.Handlers;

public class UserCacheNotificationHandler : INotificationHandler<UserCreated>
{
    public async Task Handle(UserCreated notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"OK user created. I should prepare cached data for the user {notification.UserId}");
        await Task.CompletedTask;
    }
}

