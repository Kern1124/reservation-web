using reservation_backend.Features.OfferedServices.SharedRequestBodies;
using reservation_backend.Models;

namespace reservation_backend.Features.OfferedServices.CreateService;

public class CreateServiceRequest : IServiceNameDescRequest, IServiceLocationRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public LocationDto Location { get; set; }
    public List<TimeSlotDto> TimeSlots { get; set; }
}