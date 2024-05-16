using FastEndpoints;
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
        var service = OSService.GetServiceById(req.ServiceId);
        int userId = int.Parse(HttpContext.User.Claims.First(c => c.Type == "id").Value);
        var user = UserService.GetUserById(userId);
        
        if (service == null || user == null)
        {
            AddError("Service or userId (specified by cookies) not found");
            await SendErrorsAsync();
            return;
        }

        Reservation? reservation = ReservationService.CreateUserReservation(user!, service!, req.DateStart, req.DateEnd);
        if (reservation == null)
        {
            AddError("Time slot full");
            await SendErrorsAsync();
        }
        else
        {
            Response.Message = "Successfully created reservation";
        }
    }
}