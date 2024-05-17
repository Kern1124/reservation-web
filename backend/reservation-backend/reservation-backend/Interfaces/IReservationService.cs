using reservation_backend.Models;

namespace reservation_backend.Interfaces;

public interface IReservationService
{
    public Task<Reservation> CreateUserReservation(int userId, int serviceId, DateTime dateStart, DateTime dateEnd);
    public Task<List<Reservation>> GetReservationTermsByServiceId(int id, DateTime? date = null);
    public Task<List<Reservation>> GetReservationsByUserId(int id);
    public Task<Reservation> GetReservationById(int id);
    public void RemoveReservation(Reservation reservation);

}