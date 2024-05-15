using reservation_backend.Models;
using reservation_backend.Users;

namespace reservation_backend.Features.Users.Login;

public class LoginResponse
{
    public UserDto User { get; set; }
}