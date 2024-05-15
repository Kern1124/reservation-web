namespace reservation_backend.Features.OfferedServices.SharedRequestBodies;

public interface IServiceNameDescRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
}