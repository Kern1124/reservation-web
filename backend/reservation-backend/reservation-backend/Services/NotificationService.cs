using Microsoft.EntityFrameworkCore;
using reservation_backend.Database;
using reservation_backend.Dto;
using reservation_backend.Exceptions;
using reservation_backend.Interfaces;
using reservation_backend.Models;

namespace reservation_backend.Services;

public class NotificationService : INotificationService
{
    private Context _databaseContext;
    
    public NotificationService(Context databaseContext)
    {
        _databaseContext = databaseContext;
    }
    
    public async Task<Notification> SendNotification(int userId, string subject, string message)
    {
        User user;
        try
        {
        user = await _databaseContext.Users
            .Include(u => u.Notifications)
            .FirstAsync(u => u.Id == userId);
        }
        catch (Exception)
        {
            throw new ResourceNotFoundException($"User with id: '{userId}' not found.");
        }

        var notification = new Notification(user, subject, message);
        user.Notifications.Add(notification);
        await _databaseContext.SaveChangesAsync();
        return notification;
    }
    
    public async Task<List<Notification>?> GetLastUserNotifications(int userId, int count)
    {
        try
        {
            await _databaseContext.Users
                .Include(u => u.Notifications)
                .FirstAsync(u => u.Id == userId);
        }
        catch (Exception)
        {
            throw new ResourceNotFoundException($"User with id: '{userId}' not found.");
        }

        return await _databaseContext.Notifications
            .Where(n => n.Recipient.Id == userId && !n.IsRead)
            .OrderByDescending(n => n.TimeStamp)
            .Take(count).ToListAsync();
    }
    
    public async Task<Notification> SetAsRead(int notificationId)
    {
        Notification notification;
        try
        {
            notification = await _databaseContext.Notifications
                .FirstAsync(n => n.Id == notificationId);
        }
        catch (Exception)
        {
            throw new ResourceNotFoundException("Notification not found.");
        }
        notification.IsRead = true;
        await _databaseContext.SaveChangesAsync();
        return notification;
    }

    public async Task<Notification> GetNotificationById(int id)
    {
        try
        {
            return await  _databaseContext.Notifications
                .Include(n => n.Recipient)
                .FirstAsync(n => n.Id == id);
        } catch (Exception)
        {
            throw new ResourceNotFoundException("Notification not found.");
        }
    }
    
}