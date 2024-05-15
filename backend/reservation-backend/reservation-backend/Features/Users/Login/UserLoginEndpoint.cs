using System.Net;
using FastEndpoints;
using FastEndpoints.Security;
using reservation_backend.Enums;
using reservation_backend.Interfaces;
using reservation_backend.Models;
using reservation_backend.Users;

namespace reservation_backend.Features.Users.Login;

public class UserLoginEndpoint : Endpoint<LoginRequest, LoginResponse>
{
    public IUserService UserService { get; set; }
    public override void Configure()
    {
        Post("/api/users/login");
        AllowAnonymous();
    }
    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {
        var (loginResult, user) = UserService.Login(req.MailAddress, req.Password);
        if (user == null)
        {
            switch (loginResult)
            {
                case LoginResult.Empty: AddError("Credentials can not be empty."); break;
                case LoginResult.UserNotFound: case LoginResult.PasswordIncorrect:
                    AddError("E-Mail or Password incorrect"); break;
            }
            await SendErrorsAsync();
        }
        else
        {
            await CookieAuth.SignInAsync(u =>
                {
                    u.Roles.Add("logged-user");
                    u["username"] = user.Username;
                    u["mailAddress"] = user.MailAddress;
                    u["id"] = user.Id.ToString();
                }
            );
            Response.User = new UserDto(user);
        }
    }
}