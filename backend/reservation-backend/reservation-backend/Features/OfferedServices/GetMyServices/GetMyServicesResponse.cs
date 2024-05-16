using reservation_backend.Models;

namespace reservation_backend.Features.OfferedServices.GetMyServices;

public class GetMyServicesResponse
{
    public List<OfferedServiceDto> Services { get; set; }
}