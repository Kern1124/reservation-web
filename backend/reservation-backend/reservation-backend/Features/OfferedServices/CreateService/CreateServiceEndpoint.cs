using FastEndpoints;
using reservation_backend.Exceptions;
using reservation_backend.Features.OfferedServices.Validators;
using reservation_backend.Interfaces;
using reservation_backend.Models;

namespace reservation_backend.Features.OfferedServices.CreateService;

public class CreateServiceEndpoint : Endpoint<CreateServiceRequest, CreateServiceResponse>
{
    public IOSService OSService { get; set; }
    public IUserService UserService { get; set; }

    public override void Configure()
    {
        Post("/api/services");
        Roles("logged-user");
        Options(x => x.WithTags("OfferedServices"));
        Validator<ServiceLocationValidator>();
        Validator<ServiceNameDescValidator>();
    }

    public override async Task HandleAsync(CreateServiceRequest req, CancellationToken ct)
    {
        User? user;
        try
        {
            user = await UserService.GetUserById(int.Parse(HttpContext.User.Claims.First(c => c.Type == "id").Value));
        } 
        catch (ResourceNotFoundException)
        {
            AddError("User not found");
            await SendErrorsAsync(404);
            return;
        }

        if (req.TimeSlots.Count == 0)
        {
            AddError("At least one time slot is required");
            await SendErrorsAsync();
            return;
        }

        var location = new Location(req.Location.Country, req.Location.City, req.Location.Address);
        var service = new OfferedService(
            user, req.Name, req.Description, location,
            req.TimeSlots.Select(t => new TimeSlotSpan(t.Start, t.End)).ToList());
        
        try
        {
            await OSService.AddService(service);
        }
        catch (ResourceExistsException e)
        {
            AddError($"{e.Message}");
            await SendErrorsAsync();
            return;
        }

        Response.Message = "Service created";
        Response.Service = new OfferedServiceDto(service);
        await SendOkAsync(Response,ct); 
    }
}