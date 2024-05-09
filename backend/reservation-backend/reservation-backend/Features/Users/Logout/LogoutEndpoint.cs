using FastEndpoints;
using FastEndpoints.Security;

namespace reservation_backend.Features.Users.Logout;

public class LogoutEndpoint : EndpointWithoutRequest
{
    public override void Configure()
    {
        Post("/api/users/logout");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await CookieAuth.SignOutAsync();
        await SendOkAsync(ct);
    }
}