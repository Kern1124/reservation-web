using FastEndpoints;
using reservation_backend.Interfaces;

namespace reservation_backend.Features.OfferedServices.RemoveService;

public class RemoveServiceEndpoint : Endpoint<RemoveServiceRequest, RemoveServiceResponse>
{
    public IOSService OSService { get; set; }
    
    public override void Configure()
    {
        Delete("/api/services/remove/{id}");
        Roles("logged-user");
    }

    public override async Task HandleAsync(RemoveServiceRequest req, CancellationToken ct)
    {
        int? id = Route<int>("id", isRequired: true);
        var service = OSService.GetServiceById(id.Value);
        Console.WriteLine(id);
        if (service == null)
        {
            AddError("Service not found");
            await SendErrorsAsync();
        }
        if (service?.Owner.Id != int.Parse(HttpContext.User.Claims.First(c => c.Type == "id").Value))
        {
            AddError("You don't have permission to delete this service");
            await SendErrorsAsync();
        }
        OSService.DeleteService(id.Value);
        await SendOkAsync(ct);
    }
}