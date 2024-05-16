using reservation_backend.Features.OfferedServices.Validators;
using reservation_backend.Models;

namespace reservation_backend.Dto;

public class ReservationDto
{
    public int Id { get; set; }
    public OfferedServiceDto Service { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
    public string TimeSlot { get; set; }
    
    public ReservationDto(){}
    
    public ReservationDto(Reservation reservation, OfferedService service)
    {
        Id = reservation.Id;
        Service = new OfferedServiceDto(service);
        DateStart = reservation.DateStart;
        DateEnd = reservation.DateEnd;
        TimeSlot = DateStart.ToString("HH:mm") + " - " + DateEnd.ToString("HH:mm");
    }
}