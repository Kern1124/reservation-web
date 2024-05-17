using FastEndpoints;
using reservation_backend.Exceptions;
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
        Options(x => x.WithTags("OfferedServices"));

    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        int id = Route<int>("id");
        OfferedService service;
        try
        {
            service = await OSService.GetServiceById(id);
        }
        catch (ResourceNotFoundException)
        {
            AddError("Service not found");
            await SendErrorsAsync(404);
            return;
        }
        Response.Services = [new(service)];
        await SendOkAsync(Response, ct);
    }
}