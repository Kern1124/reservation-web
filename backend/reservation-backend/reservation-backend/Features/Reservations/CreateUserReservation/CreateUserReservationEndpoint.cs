using FastEndpoints;
using reservation_backend.Exceptions;
using reservation_backend.Interfaces;
using reservation_backend.Models;
using reservation_backend.Services;

namespace reservation_backend.Features.Reservations.CreateUserReservation;

public class CreateUserReservationEndpoint : Endpoint<CreateUserReservationRequest, CreateUserReservationResponse>
{
    public IUserService UserService { get; set; }
    public IOSService OSService { get; set; }
    public IReservationService ReservationService { get; set; }


    public override void Configure()
    {
        Post("/api/reservations");
        Roles("logged-user");
        Options(x => x.WithTags("Reservations"));
    }
    public override async Task HandleAsync(CreateUserReservationRequest req, CancellationToken ct)
    {
        int userId = int.Parse(HttpContext.User.Claims.First(c => c.Type == "id").Value);
        try
        {
            await ReservationService.CreateUserReservation(userId, req.ServiceId, req.DateStart, req.DateEnd);
        }
        catch (ResourceNotFoundException)
        {
            AddError("Service or user not found");
            await SendErrorsAsync(404);
            return;
        }
        catch (ResourceExistsException)
        {
            AddError("Time slot full");
            await SendErrorsAsync();
            return;
        }
        Response.Message = "Successfully created reservation";
    }
}