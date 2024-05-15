using FastEndpoints;
using reservation_backend.Features.Users.Login;
using reservation_backend.Models;
using reservation_backend.Users;

namespace reservation_backend.Features.Users.Authenticated;

public class AuthenticatedEndpoint : EndpointWithoutRequest<AuthenticatedResponse>
{
    public override void Configure()
    {
        Get("/api/users/authenticated");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var user = new UserDto(HttpContext.User.Claims.First(c => c.Type == "username").Value, 
            HttpContext.User.Claims.First(c => c.Type == "mailAddress").Value);
        Response.User = user;
        await SendOkAsync(Response, ct);
    }
}