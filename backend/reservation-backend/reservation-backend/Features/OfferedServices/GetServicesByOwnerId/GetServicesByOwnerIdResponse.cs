using reservation_backend.Models;

namespace reservation_backend.Features.OfferedServices.GetServicesByOwnerId;

public class GetServicesByOwnerIdResponse
{
    public List<OfferedServiceDto> Services { get; set; }
}