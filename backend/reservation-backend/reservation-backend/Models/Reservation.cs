using System.Runtime.InteropServices.JavaScript;
using reservation_backend.Users;

namespace reservation_backend.Models;

public class Reservation
{
    public int Id { get; set; }
    public User User { get; set; }
    public OfferedService OfferedService { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
    public Reservation(DateTime dateStart, DateTime dateEnd)
    {
        DateStart = dateStart;
        DateEnd = dateEnd;
    }

    public Reservation(User user, OfferedService offeredService, DateTime dateStart, DateTime dateEnd)
    {
        User = user;
        OfferedService = offeredService;
        DateStart = dateStart;
        DateEnd = dateEnd;
    }
}