using LeadSoft.Core.Notifications;

namespace LeadSoft.Core.Interfaces.Notifications;

public interface INotify
{
    bool IsEmptyNotification();
    List<Notification> GetNotifications();
    void AddNotification(Notification notification);
}