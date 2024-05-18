using reservation_backend.Dto;

namespace reservation_backend.Features.OfferedServices.GetServiceReservations;

public class GetServiceReservationsResponse
{
    public List<ReservationDto> Reservations { get; set; }
}