using reservation_backend.Models;

namespace reservation_backend.Interfaces;

public interface IOSService
{
    public Task<List<TimeSlotStateDto>> GetTimeSlotsByServiceIdAndDate(int id, DateTime date);
    public Task<List<OfferedService>> GetServicesByOwnerId(int id);

    public Task<List<OfferedService>> GetAllServices();
    public Task<List<Reservation>> GetServiceReservations(int id);
    public Task<OfferedService> GetServiceById(int id);
    public Task<OfferedService> AddService(OfferedService service);
    public Task<int> UpdateServiceImage(int id, IFormFile img);
    public Task<int> DeleteService(int id);
    public Task<int> UpdateService(OfferedService service, (string? name, string? desc) newServiceDetails);
}