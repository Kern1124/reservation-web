using FastEndpoints;
using reservation_backend.Database;
using reservation_backend.Features.OfferedServices.Validators;
using reservation_backend.Interfaces;
using reservation_backend.Models;

namespace reservation_backend.Features.OfferedServices.CreateService;

public class CreateServiceEndpoint : Endpoint<CreateServiceRequest, CreateServiceResponse>
{
    public IOSService OSService { get; set; }
    public Context DatabaseContext { get; set; }

    public override void Configure()
    {
        Post("/api/services");
        Roles("logged-user");
        Validator<ServiceLocationValidator>();
        Validator<ServiceNameDescValidator>();
    }

    public override async Task HandleAsync(CreateServiceRequest req, CancellationToken ct)
    {
        User? user = null;
        try
        {
            user = DatabaseContext.Users.FirstOrDefault(
                u => u.Id.ToString() == HttpContext.User.FindFirst("id")!.Value);
        }
        catch (Exception e)
        {
            AddError("Unexpected error");
            await SendErrorsAsync();
        }
        
        if (user == null)
        {
            AddError("User not found, incorrect claim");
            await SendErrorsAsync();
        }
        var location = new Location(req.Location.Country, req.Location.City, req.Location.Address);
        var service = new OfferedService(user!, req.Name, req.Description, location);
        OSService.AddService(service);
        Response.Message = "Service created";
        await SendOkAsync(ct);
    }
}