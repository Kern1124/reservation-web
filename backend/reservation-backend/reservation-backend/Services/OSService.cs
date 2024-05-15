using System.Globalization;
using System.Xml;
using Microsoft.EntityFrameworkCore;
using reservation_backend.Database;
using reservation_backend.Interfaces;
using reservation_backend.Models;

namespace reservation_backend.Services;

public class OSService : IOSService
{
    private readonly Context _databaseContext;
    public OSService(Context databaseContext)
    {
        _databaseContext = databaseContext;
    }

    private static TimeSlotStateDto CheckAvailabilityAndReturnState(int id, DateTime date, TimeSlotSpan timeSlot, List<Reservation> existingReservations)
    {
        string format = "HH:mm";
        var ptStart = DateTime.ParseExact(timeSlot.Start, format, CultureInfo.CurrentCulture).TimeOfDay;
        var ptEnd = DateTime.ParseExact(timeSlot.End, format, CultureInfo.CurrentCulture).TimeOfDay;
        Console.WriteLine(existingReservations.Count);
        existingReservations.ForEach(r => Console.WriteLine($"{ptStart} - {ptEnd} | {r.DateStart} - {r.DateEnd}"));
        bool available = !existingReservations.Any(r => ptStart.TotalSeconds <= r.DateEnd.TimeOfDay.TotalSeconds &&
                                                        r.DateStart.TimeOfDay.TotalSeconds <= ptEnd.TotalSeconds);
        return new TimeSlotStateDto(timeSlot.Start, timeSlot.End, available, null);
    }
    
    public List<OfferedService> GetAllServices()
    {
        return _databaseContext.OfferedServices
            .Include(s => s.Owner)
            .Include(s => s.Location)
            .ToList();
    }
    
    public OfferedService? GetServiceById(int id)
    {
        return _databaseContext.OfferedServices
            .Include(s=> s.Owner)
            .Include(s => s.Location)
            .FirstOrDefault(s => s.Id == id);
    }

    public bool AddService(OfferedService service)
    {
        _databaseContext.OfferedServices.Add(service);
        _databaseContext.SaveChanges();
        return true;
    }

    public bool UpdateService(OfferedService service, (string? name, string? desc) newServiceDetails)
    {
        var entity = _databaseContext.OfferedServices.Update(service).Entity;
        if (newServiceDetails.name != null)
        {
            entity.Name = newServiceDetails.name;
        }
        if (newServiceDetails.desc != null)
        {
            entity.Description = newServiceDetails.desc;
        }
        _databaseContext.SaveChanges();
        return true;
    }
    public bool DeleteService(int id)
    {
        OfferedService? foundService;
        if (null == (foundService = _databaseContext.OfferedServices.FirstOrDefault(s => s.Id == id)))
        {
            return false;
        }
        _databaseContext.OfferedServices.Remove(foundService);
        _databaseContext.SaveChanges();
        return true;
    }
    
    public List<TimeSlotStateDto> GetTimeSlotsByServiceIdAndDate(int id, DateTime date)
    {
        var existingReservations = _databaseContext.Reservations
            .Where(r => r.OfferedService.Id == id && r.DateStart.Date == date.Date)
            .ToList();
        List<TimeSlotStateDto> result = _databaseContext.OfferedServices
            .Include(s => s.TimeSlots)
            .SelectMany(s => s.TimeSlots)
            .Select(t => CheckAvailabilityAndReturnState(id, date, t, existingReservations)).ToList();
        return result;
    }
    
    public List<OfferedService> GetServicesByOwnerId(int id)
    {
        var result = _databaseContext.OfferedServices
            .Include(s => s.Owner)
            .Where(s => s.Owner.Id == id).Include(s=> s.Location).ToList();
        return result;

    }
}