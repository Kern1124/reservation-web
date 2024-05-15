using FastEndpoints;
using reservation_backend.Interfaces;
using reservation_backend.Models;

namespace reservation_backend.Features.OfferedServices.GetServices;

public class GetServiceByIdEndpoint : EndpointWithoutRequest<GetServicesResponse>
{
    public IOSService OSService { get; set; }
    public override void Configure()
    {
        Get("/api/services/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        int id = Route<int>("id");
        OfferedService? service = OSService.GetServiceById(id);
        if (service == null)
        {
            AddError("Service not found");
            await SendErrorsAsync();
        }
        else
        {
            Response.Services = new List<OfferedServiceDto> {new (service)};
            await SendOkAsync(Response, ct);
        }
        
    }
}