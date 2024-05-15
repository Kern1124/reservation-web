using FastEndpoints;
using reservation_backend.Database;
using reservation_backend.Interfaces;
using reservation_backend.Models;

namespace reservation_backend.Features.OfferedServices.GetServicesByOwnerId;

public class GetServicesByOwnerIdEndpoint : EndpointWithoutRequest<GetServicesByOwnerIdResponse>
{
    public Context DatabaseContext { get; set; }
    public IOSService OSService { get; set; }
    public override void Configure()
    {
        Get("/api/services/owner");
        Roles("logged-user");

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