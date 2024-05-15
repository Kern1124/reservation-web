using reservation_backend.Models;

namespace reservation_backend.Models;

public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string MailAddress { get; set; }
    
    public UserDto(User user)
    {
        Id = user.Id;
        Username = user.Username;
        MailAddress = user.MailAddress;
    }
    public UserDto(string username, string mailAddress)
    {
        Username = username;
        MailAddress = mailAddress;
    }
}