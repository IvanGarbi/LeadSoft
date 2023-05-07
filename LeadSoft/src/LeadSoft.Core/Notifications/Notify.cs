using LeadSoft.Core.Interfaces.Notifications;

namespace LeadSoft.Core.Notifications;

public class Notify : INotify
{
    private List<Notification> _notifications;

    public Notify()
    {
        _notifications = new List<Notification>();
    }

    public bool IsEmptyNotification()
    {
        return !_notifications.Any();
    }

    public List<Notification> GetNotifications()
    {
        return _notifications;
    }

    public void AddNotification(Notification notification)
    {
        _notifications.Add(notification);
    }
}