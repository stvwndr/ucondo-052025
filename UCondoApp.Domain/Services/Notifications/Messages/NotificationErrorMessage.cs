namespace UCondoApp.Domain.Services.Notifications.Messages;

public class NotificationErrorMessage
{
    public string Type { get; set; } = "Error";
    public IEnumerable<NotificationMessage>? Details { get; set; }
}
