using MediatR;
using RYB.MediatR.Notifications;

namespace RYB.MediatR.Handlers;

public class UserNotificationHandler : INotificationHandler<UserCreated>
{
    public async Task Handle(UserCreated notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"OK user created. I will take care of furthur process : {notification.UserId}");
        await Task.CompletedTask;
    }
}

