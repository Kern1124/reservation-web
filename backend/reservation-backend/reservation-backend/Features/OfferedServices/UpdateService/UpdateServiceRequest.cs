using reservation_backend.Features.OfferedServices.SharedRequestBodies;

namespace reservation_backend.Features.OfferedServices.UpdateService;

public class UpdateServiceRequest : IServiceNameDescRequest
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}