using FastEndpoints;
using reservation_backend.Database;
using reservation_backend.Features.OfferedServices.GetMyServices;
using reservation_backend.Interfaces;
using reservation_backend.Models;

namespace reservation_backend.Features.OfferedServices.GetMyServices;

public class GetMyServicesEndpoint : EndpointWithoutRequest<GetMyServicesResponse>
{
    public Context DatabaseContext { get; set; }
    public IOSService OSService { get; set; }
    public override void Configure()
    {
        Get("/api/services/my");
        Roles("logged-user");
        Options(x => x.WithTags("OfferedServices"));

    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        int ownerId = int.Parse(HttpContext.User.Claims.First(c => c.Type == "id").Value);
        Response.Services = OSService.GetServicesByOwnerId(ownerId)
            .Select(s => new OfferedServiceDto(s))
            .ToList();
        await SendOkAsync(Response, ct);
    }
}