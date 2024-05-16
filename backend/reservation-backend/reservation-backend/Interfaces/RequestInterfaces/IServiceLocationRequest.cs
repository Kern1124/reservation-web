using reservation_backend.Models;

namespace reservation_backend.Features.OfferedServices.SharedRequestBodies;

public interface IServiceLocationRequest
{
    public LocationDto Location { get; set; }
}