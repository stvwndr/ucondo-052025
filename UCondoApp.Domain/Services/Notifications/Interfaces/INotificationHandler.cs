using UCondoApp.Domain.Services.Notifications.Messages;

namespace UCondoApp.Domain.Services.Notifications.Interfaces;

public interface INotificationHandler
{
    IReadOnlyCollection<NotificationMessage> Notifications { get; }
    NotificationErrorMessage NotificationResponse { get; }
    bool HasNotifications { get; }

    void AddNotification(string message);
}
