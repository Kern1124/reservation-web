using FastEndpoints;
using reservation_backend.Interfaces;
using reservation_backend.Models;

namespace reservation_backend.Features.OfferedServices.GetServices;

public class GetAllServicesEndpoint : EndpointWithoutRequest<GetServicesResponse>
{
    public IOSService OSService { get; set; }
    public override void Configure()
    {
        Get("/api/services");
        AllowAnonymous();
        Options(x => x.WithTags("OfferedServices"));

    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        
        Response.Services = OSService.GetAllServices().Select(s => new OfferedServiceDto(s)).ToList();
        await SendOkAsync(Response, ct);
    }
}