using UCondoApp.Domain.Services.Notifications.Interfaces;
using UCondoApp.Domain.Services.Notifications.Messages;

namespace UCondoApp.Domain.Services.Notifications;

public class NotificationHandler : INotificationHandler
{
    private readonly List<NotificationMessage> _notifications;

    public NotificationHandler()
    {
        _notifications = new List<NotificationMessage>();
    }

    public IReadOnlyCollection<NotificationMessage> Notifications => _notifications;
    public NotificationErrorMessage NotificationResponse => new NotificationErrorMessage
    {
        Details = Notifications
    };
    public bool HasNotifications => _notifications.Any();


    public void AddNotification(string message)
    {
        _notifications.Add(new NotificationMessage(message));
    }
}
