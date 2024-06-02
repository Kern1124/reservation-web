using reservation_backend.Models;

namespace reservation_backend.Features.OfferedServices.GetServiceTimeSlots;

public class GetTimeSlotStatesResponse
{
    public List<TimeSlotStateDto> TimeSlots { get; set; }
}