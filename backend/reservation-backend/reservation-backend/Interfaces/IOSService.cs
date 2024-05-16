using reservation_backend.Models;

namespace reservation_backend.Interfaces;

public interface IOSService
{
    public List<TimeSlotStateDto>? GetTimeSlotsByServiceIdAndDate(int id, DateTime date);
    public List<OfferedService> GetServicesByOwnerId(int id);

    public List<OfferedService> GetAllServices();
    public OfferedService? GetServiceById(int id);
    public OfferedService? AddService(OfferedService service);
    public bool DeleteService(int id);
    public bool UpdateService(OfferedService service, (string? name, string? desc) newServiceDetails);
}