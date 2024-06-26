using FastEndpoints;
using reservation_backend.Exceptions;
using reservation_backend.Interfaces;
using reservation_backend.Models;

namespace reservation_backend.Features.OfferedServices.RemoveService;

public class RemoveServiceEndpoint : Endpoint<RemoveServiceRequest, RemoveServiceResponse>
{
    public IOSService OSService { get; set; }
    
    public override void Configure()
    {
        Delete("/api/services/remove/{id}");
        Roles("logged-user");
        Options(x => x.WithTags("OfferedServices"));

    }

    public override async Task HandleAsync(RemoveServiceRequest req, CancellationToken ct)
    {
        int? id = Route<int>("id", isRequired: true);
        OfferedService? service = null;
        try
        {
            service = await OSService.GetServiceById(id.Value);
        }
        catch (ResourceNotFoundException)
        {
            AddError("Service not found");
            await SendErrorsAsync(404);
            return;

        }

        if (service!.Owner.Id != int.Parse(HttpContext.User.Claims.First(c => c.Type == "id").Value))
        {
            AddError("You don't have permission to delete this service");
            await SendErrorsAsync(403);
        }
        else
        {
            await OSService.DeleteService(id.Value);
            await SendOkAsync(ct);
        }
    }
}