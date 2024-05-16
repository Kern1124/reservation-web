using FastEndpoints;
using reservation_backend.Interfaces;

namespace reservation_backend.Features.Reservations.RemoveReservation;

public class RemoveReservationEndpoint : Endpoint<RemoveReservationRequest, RemoveReservationResponse>
{
    
    public IReservationService ReservationService { get; set; }

    public override void Configure()
    {
        Delete("/api/reservations/{id}");
        Roles("logged-user");
        Options(x => x.WithTags("Reservations"));
    }
    public override async Task HandleAsync(RemoveReservationRequest req, CancellationToken ct)
    {
        var reservation = ReservationService
            .GetReservationById(req.Id);
        if (reservation == null)
        {
            AddError("Reservation not found");
            await SendErrorsAsync(404);
            return;
        }
        
        int userId = int.Parse(HttpContext.User.Claims.First(c => c.Type == "id").Value);
        if (userId != reservation!.User.Id && userId != reservation.OfferedService.Owner.Id)
        {
            AddError("Not authorized to remove reservation");
            await SendErrorsAsync(403);
        }
        else
        {
            ReservationService.RemoveReservation(reservation!);
            Response.Message = "Reservation removed successfully"; 
        }
    }
}