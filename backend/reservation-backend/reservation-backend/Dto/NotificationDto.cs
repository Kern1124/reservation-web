using reservation_backend.Models;

namespace reservation_backend.Dto;

public class NotificationDto
{
    public int Id { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }
    public string Timestamp { get; set; }
    public bool IsRead { get; set; }

    public NotificationDto()
    {
    }

    public NotificationDto(Notification notification)
    {
        Id = notification.Id;
        Subject = notification.Subject;
        Content = notification.Content;
        Timestamp = notification.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss");
        IsRead = notification.IsRead;
    }

    public NotificationDto(string subject, string content)
    {
        Subject = subject;
        Content = content;
    }
}