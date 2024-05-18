using System.Globalization;
using Microsoft.EntityFrameworkCore;
using reservation_backend.Database;
using reservation_backend.Exceptions;
using reservation_backend.Interfaces;
using reservation_backend.Models;
using Serilog.Core;

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
        bool available = !existingReservations.Any(r => ptStart.TotalSeconds <= r.DateEnd.TimeOfDay.TotalSeconds &&
                                                        r.DateStart.TimeOfDay.TotalSeconds <= ptEnd.TotalSeconds);
        bool blocked = date.Date.AddHours(ptStart.Hours).AddMinutes(ptStart.Minutes) < DateTime.Now;
        return new TimeSlotStateDto(timeSlot.Start, timeSlot.End, available, null, blocked);
    }
    
    public async Task<List<OfferedService>> GetAllServices()
    {
        return await _databaseContext.OfferedServices
            .Include(s => s.Owner)
            .Include(s => s.Location)
            .ToListAsync();
    }

    public async Task<OfferedService> GetServiceById(int id)
    {
        try
        {
            return await _databaseContext.OfferedServices
                .Include(s => s.Owner)
                .Include(s => s.Location)
                .FirstAsync(s => s.Id == id);
        }
        catch (Exception)
        {
            throw new ResourceNotFoundException($"Service with id: '{id}' not found.");
        }
    }

    public async Task<OfferedService> AddService(OfferedService service)
    {
        OfferedService? result;
        try
        {
            if (_databaseContext.OfferedServices
                .Include(s => s.Location)
                .Any(s => s.Name == service.Name && s.Location.Country == service.Location.Country))
            {
                throw new ResourceExistsException("Service with this name and country already exists");
            }
            result = _databaseContext.OfferedServices.Add(service).Entity;
            await _databaseContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new ResourceExistsException("Service with this name and country already exists");
        }
        return result;
    }

    public async void UpdateService(OfferedService service, (string? name, string? desc) newServiceDetails)
    {
        OfferedService entity;
        try
        {
            entity = _databaseContext.OfferedServices.Update(service).Entity;
        }
        catch (Exception)
        {
            throw new ResourceUpdateFailedException($"Service with id: '{service.Id}' not found.");
        }
        
        if (newServiceDetails.name != null)
        {
            if (_databaseContext.OfferedServices
                .Include(s => s.Location)
                .Any(s => s.Name == newServiceDetails.name && s.Location.Country == service.Location.Country))
            {
                throw new ResourceExistsException("Service with this name and country already exists");
            }
            entity.Name = newServiceDetails.name;
        }

        if (newServiceDetails.desc != null)
        {
            entity.Description = newServiceDetails.desc;
        }

        await _databaseContext.SaveChangesAsync();
    }

    public async void DeleteService(int id)
    {
        OfferedService? foundService;
        try
        {
            foundService = await _databaseContext.OfferedServices
                .FirstAsync(s => s.Id == id);
        }
        catch (Exception)
        {
            throw new ResourceNotFoundException($"Service with id: '{id}' not found.");
        }
        _databaseContext.OfferedServices.Remove(foundService);
        await _databaseContext.SaveChangesAsync();
    }
    
    public async Task<List<TimeSlotStateDto>> GetTimeSlotsByServiceIdAndDate(int id, DateTime date)
    {
        List<TimeSlotStateDto>? result;
        try
        {
            var existingReservations = await _databaseContext.Reservations
                .Where(r => r.OfferedService.Id == id && r.DateStart.Date == date.Date)
                .ToListAsync();
            var service = await _databaseContext.OfferedServices
                .FirstOrDefaultAsync(s => s.Id == id);
            // We want to throw if service doesn't exist. Therefore we do not use the nullable operator.
            // The warning on TimeSlots is false.
            result = service!.TimeSlots
                .Select(t => CheckAvailabilityAndReturnState(id, date, t, existingReservations))
                .ToList();
            Console.WriteLine(result.Count);
        }
        catch (Exception)
        {
            throw new ResourceNotFoundException($"Service with id: '{id}' not found.");
        }
        return result;
    }
    
    public async Task<List<OfferedService>> GetServicesByOwnerId(int id)
    {
        var result = await _databaseContext.OfferedServices
            .Include(s => s.Owner)
            .Where(s => s.Owner.Id == id).Include(s=> s.Location).ToListAsync();
        return result;

    }
    
    public async Task<List<Reservation>> GetServiceReservations(int id)
    {
        var result = await _databaseContext.Reservations
            .Include(r => r.OfferedService)
            .Include(r => r.User)
            .Where(r => r.OfferedService.Id == id)
            .ToListAsync();
        return result;
    }
}