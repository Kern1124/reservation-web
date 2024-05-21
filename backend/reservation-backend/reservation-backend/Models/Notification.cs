namespace reservation_backend.Models;

public class Notification
{
    public int Id { get; set; }
    public User Recipient { get; set; }
    public DateTime TimeStamp { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }
    public bool IsRead { get; set; }
    
    public Notification(string subject, string content)
    {
        Content = content;
        TimeStamp = DateTime.Now;
        IsRead = false;
        Subject = subject;
    }

    public Notification(User recipient, string subject, string content)
    {
        Content = content;
        TimeStamp = DateTime.Now;
        Subject = subject;
        IsRead = false;
        Recipient = recipient;
    }
}