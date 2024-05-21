using reservation_backend.Database;
using reservation_backend.Models;

namespace reservation_backend.Interfaces;

public interface INotificationService
{

    public Task<Notification> SendNotification(int userId, string subject, string message);
    public Task<Notification> SetAsRead(int notificationId);
    public Task<List<Notification>?> GetLastUserNotifications(int userId, int count);
    public Task<Notification> GetNotificationById(int notificationId);
}