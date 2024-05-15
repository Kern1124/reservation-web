using FastEndpoints;
using reservation_backend.Features.OfferedServices.Validators;
using reservation_backend.Interfaces;
using reservation_backend.Models;

namespace reservation_backend.Features.OfferedServices.UpdateService;

public class UpdateServiceEndpoint : Endpoint<UpdateServiceRequest, UpdateServiceResponse>
{

    public IOSService _osService { get; set; }
    public override void Configure()
    {
        Put("/api/services/update/{id}");
        Roles("logged-user");
        Validator<ServiceNullableNameDescValidator>();
    }

    public override async Task HandleAsync(UpdateServiceRequest req, CancellationToken ct)
    {
        int? id = Route<int>("id", isRequired: true);
        var service = _osService.GetServiceById(id.Value);
        if (service == null)
        {
            AddError("Service not found");
            await SendErrorsAsync();
        }
        if (service?.Owner.Id != int.Parse(HttpContext.User.Claims.First(c => c.Type == "id").Value))
        {
            AddError("You don't have permission to update this service");
            await SendErrorsAsync();
        }
        var newServiceDetails = (req.Name, req.Description);
        _osService.UpdateService(service!, newServiceDetails);
        await SendOkAsync(ct);
    }
    
}