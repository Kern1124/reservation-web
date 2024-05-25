using reservation_backend.Models;

namespace reservation_backend.Features.OfferedServices.CreateService;

public class CreateServiceResponse
{
    public OfferedServiceDto Service { get; set; } 
    public string Message { get; set; }
}