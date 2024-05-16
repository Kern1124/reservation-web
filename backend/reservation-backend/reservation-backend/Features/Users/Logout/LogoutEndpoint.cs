using FastEndpoints;
using FastEndpoints.Security;

namespace reservation_backend.Features.Users.Logout;

public class LogoutEndpoint : EndpointWithoutRequest
{
    public override void Configure()
    {
        Post("/api/users/logout");
        Options(x => x.WithTags("Users"));
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await CookieAuth.SignOutAsync();
        await SendOkAsync(ct);
    }
}