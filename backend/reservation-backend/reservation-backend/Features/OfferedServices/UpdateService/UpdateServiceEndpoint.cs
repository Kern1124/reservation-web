using FastEndpoints;
using reservation_backend.Features.OfferedServices.Validators;
using reservation_backend.Interfaces;
using reservation_backend.Models;

namespace reservation_backend.Features.OfferedServices.UpdateService;

public class UpdateServiceEndpoint : Endpoint<UpdateServiceRequest, UpdateServiceResponse>
{

    public IOSService OSService { get; set; }
    public override void Configure()
    {
        Put("/api/services/update/{id}");
        Roles("logged-user");
        Options(x => x.WithTags("OfferedServices"));
        Validator<ServiceNullableNameDescValidator>();
    }

    public override async Task HandleAsync(UpdateServiceRequest req, CancellationToken ct)
    {
        int? id = Route<int>("id", isRequired: true);
        var service = OSService.GetServiceById(id.Value);
        if (service == null)
        {
            AddError("Service not found");
            await SendErrorsAsync();
        }
        else if (service?.Owner.Id != int.Parse(HttpContext.User.Claims.First(c => c.Type == "id").Value))
        {
            AddError("You don't have permission to update this service");
            await SendErrorsAsync();
        }
        else
        {
            var newServiceDetails = (req.Name, req.Description);
            OSService.UpdateService(service!, newServiceDetails);
            await SendOkAsync(ct);
        }
    }
    
}