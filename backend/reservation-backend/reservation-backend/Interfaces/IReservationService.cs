using reservation_backend.Models;

namespace reservation_backend.Interfaces;

public interface IReservationService
{
    public Reservation? CreateUserReservation(User user, OfferedService service, DateTime dateStart, DateTime dateEnd);
    public List<Reservation> GetReservationTermsByServiceId(int id, DateTime? date = null);
    public List<Reservation> GetReservationsByUserId(int id);
    public Reservation? GetReservationById(int id);
    public void RemoveReservation(Reservation reservation);

}