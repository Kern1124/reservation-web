namespace reservation_backend.Features.Reservations.CreateUserReservation;

public class CreateUserReservationRequest
{
    public int ServiceId { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
}