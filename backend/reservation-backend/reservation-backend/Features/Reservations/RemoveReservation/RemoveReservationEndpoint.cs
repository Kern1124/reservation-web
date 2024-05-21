using FastEndpoints;
using reservation_backend.Exceptions;
using reservation_backend.Interfaces;
using reservation_backend.Models;
using reservation_backend.Services;

namespace reservation_backend.Features.Reservations.RemoveReservation;

public class RemoveReservationEndpoint : Endpoint<RemoveReservationRequest, RemoveReservationResponse>
{
    
    public IReservationService ReservationService { get; set; }
    public INotificationService NotificationService { get; set; }

    public override void Configure()
    {
        Delete("/api/reservations/{id}");
        Roles("logged-user");
        Options(x => x.WithTags("Reservations"));
    }
    public override async Task HandleAsync(RemoveReservationRequest req, CancellationToken ct)
    {
        Reservation reservation;
        try
        {
            reservation = await ReservationService
                .GetReservationById(req.Id);
        }
        catch (ResourceNotFoundException)
        {
            AddError("Reservation not found");
            await SendErrorsAsync(404);
            return;
        }
        
        int userId = int.Parse(HttpContext.User.Claims.First(c => c.Type == "id").Value);
        bool isOwner = userId == reservation.OfferedService.Owner.Id;
        if (userId != reservation!.User.Id && userId != reservation.OfferedService.Owner.Id)
        {
            AddError("Not authorized to remove reservation");
            await SendErrorsAsync(403);
        }
        else
        {
            var message = isOwner ?
                $"User {reservation.User.Username} has cancelled his reservation" :
                $"Your reservation for service {reservation.OfferedService.Name} has been cancelled";
            
            await NotificationService.SendNotification(userId,
                $"Reservation - {reservation.OfferedService.Name}",
                message);
            ReservationService.RemoveReservation(reservation!);
            Response.Message = "Reservation removed successfully"; 
        }
    }
}