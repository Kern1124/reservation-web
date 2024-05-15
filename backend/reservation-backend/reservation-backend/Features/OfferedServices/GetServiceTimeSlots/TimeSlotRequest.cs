namespace reservation_backend.Features.OfferedServices.GetServiceTimeSlots;

public class TimeSlotRequest
{
    public int ServiceId { get; set; }
    public DateTime Date { get; set; }
}