namespace reservation_backend.Users;

public class UserDto
{
    public string Username { get; set; }
    public string MailAddress { get; set; }
    
    public UserDto(User user)
    {
        Username = user.Username;
        MailAddress = user.MailAddress;
    }
    public UserDto(string username, string mailAddress)
    {
        Username = username;
        MailAddress = mailAddress;
    }
}