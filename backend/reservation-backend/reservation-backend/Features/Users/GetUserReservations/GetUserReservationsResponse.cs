using reservation_backend.Dto;

namespace reservation_backend.Features.Users.GetUserReservations;

public class GetUserReservationsResponse
{
    public List<ReservationDto> Reservations { get; set; }
}