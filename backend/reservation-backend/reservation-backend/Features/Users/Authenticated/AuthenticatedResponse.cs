using reservation_backend.Models;
using reservation_backend.Users;

namespace reservation_backend.Features.Users.Authenticated;

public class AuthenticatedResponse
{
    public UserDto User { get; set; }
}