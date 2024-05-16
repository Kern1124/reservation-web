using System.Runtime.InteropServices.JavaScript;
using Microsoft.EntityFrameworkCore;
using reservation_backend.Database;
using reservation_backend.Interfaces;
using reservation_backend.Models;

namespace reservation_backend.Services;

public class ReservationService : IReservationService
{   
    private Context _databaseContext;

    public ReservationService(Context databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public Reservation? CreateUserReservation(User user, OfferedService service, DateTime dateStart, DateTime dateEnd)
    {
        bool conflict = _databaseContext.OfferedServices.SelectMany(
            s => s.Reservations).Any(r => dateStart <= r.DateEnd && r.DateStart <= dateEnd);
        if (conflict)
        {
            return null;
        }
        Reservation res = new Reservation(user, service, dateStart, dateEnd);
        _databaseContext.Reservations.Add(res);
        _databaseContext.SaveChanges();
        return res;
    }

    public List<Reservation> GetReservationTermsByServiceId(int id, DateTime? date = null)
    {
        var reservations = _databaseContext.Reservations
            .Where(r => r.OfferedService.Id == id)
            .ToList();

        if (date != null)
        {
            reservations = reservations
                .Where(r => r.DateStart.Date == date.Value.Date)
                .ToList();
        }

        return reservations;
    }

    public List<Reservation> GetReservationsByUserId(int id)
    {
        return _databaseContext.Reservations
            .Include(r => r.OfferedService)
            .Include(r => r.OfferedService.Location)
            .Include(r => r.User)
            .Where(r => r.User.Id == id).ToList();
    }
    
    public Reservation? GetReservationById(int id)
    {
        Reservation? result;
        try
        {
            result = _databaseContext.Reservations
                .Include(r => r.OfferedService)
                .Include(r => r.OfferedService.Owner)
                .Include(r => r.OfferedService.Location)
                .Include(r => r.User)
                .FirstOrDefault(r => r.Id == id);

        }
        catch (Exception)
        {
            result = null;
        }

        return result;
    }
    
    public void RemoveReservation(Reservation reservation)
    {
        _databaseContext.Reservations.Remove(reservation);
        _databaseContext.SaveChanges();
    }
}