namespace reservation_backend.Users.Requests;

public class RegisterRequest
{
    public string MailAddress { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}