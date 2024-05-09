using FastEndpoints;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using reservation_backend.Database;
using reservation_backend.Enums;
using reservation_backend.Exceptions;
using reservation_backend.Interfaces;
using reservation_backend.Services;
using reservation_backend.Users.Requests;
using reservation_backend.Users.Responses;
using Exception = System.Exception;

namespace reservation_backend.Users;

public class UserRegisterEndpoint : Endpoint<RegisterRequest, RegisterResponse>
{
    public IUserService UserService { get; set; }
    public override void Configure()
    {
        Post("/api/users/register");
        AllowAnonymous();
    }
    public override async Task HandleAsync(RegisterRequest req, CancellationToken ct)
    {
        User user = new User(req.Username, req.MailAddress);

        var (resultEnum, userCreated) = UserService.Register(user, req.Password);
        if (userCreated == null)
        {
            switch (resultEnum){
                case RegisterResult.UserMailAlreadyExists:
                    AddError("User with the same e-mail already exists.");
                    break;
                case RegisterResult.InvalidMail:
                    AddError("E-mail is invalid.");
                    break;
                case RegisterResult.UsernameAlreadyExists:
                    AddError("User with the same username already exists.");
                    break;
                case RegisterResult.Empty:
                    AddError("Credentials can not be empty.");
                    break;
            }  
            await SendErrorsAsync();
        }
        else
        {
            Response = new() { Message = "User successfully registered. You can now log in." };
        }
    }
}