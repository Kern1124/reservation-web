using reservation_backend.Models;

namespace reservation_backend.Features.OfferedServices.GetServiceTimeSlots;

public class TimeSlotResponse
{
    public List<TimeSlotStateDto> TimeSlots { get; set; }
}