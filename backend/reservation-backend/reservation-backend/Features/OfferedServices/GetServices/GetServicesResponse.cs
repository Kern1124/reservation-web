using reservation_backend.Models;

namespace reservation_backend.Features.OfferedServices.GetServices;

public class GetServicesResponse
{
    public List<OfferedServiceDto> Services { get; set; }
}