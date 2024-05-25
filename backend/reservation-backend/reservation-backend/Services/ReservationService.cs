using System.Runtime.InteropServices.JavaScript;
using Microsoft.EntityFrameworkCore;
using reservation_backend.Database;
using reservation_backend.Enums;
using reservation_backend.Exceptions;
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
    private async Task<Reservation?> GetServiceReservationByDateAndTime(int id, DateTime dateStart, DateTime dateEnd)
    {
        var result = await _databaseContext.OfferedServices
            .Include(s => s.Reservations)
            .Where(s => s.Id == id).SelectMany(s => s.Reservations)
            .FirstOrDefaultAsync(r => dateStart <= r.DateEnd && r.DateStart <= dateEnd);
        return result;
    }
    public async Task<Reservation> CreateUserReservation(int userId, int serviceId, DateTime dateStart, DateTime dateEnd)
    {
        OfferedService service;
        User user;
        try
        {
            service = await _databaseContext.OfferedServices
                .Include(s => s.TimeSlots)
                .Include(s => s.Owner)
                .Include(s => s.Location)
                .FirstAsync(s => s.Id == serviceId);
            user = await _databaseContext.Users.FirstAsync(u => u.Id == userId);
        }
        catch (Exception)
        {
            throw new ResourceNotFoundException("Service or user not found.");
        }
        
        var conflictingReservation = await GetServiceReservationByDateAndTime(userId, dateStart, dateEnd);
        if (conflictingReservation != null)
        {
            throw new ResourceExistsException("Reservation time slot full");
        }
        Reservation res = new Reservation(user!, service, dateStart, dateEnd);
        _databaseContext.Reservations.Add(res);
        await _databaseContext.SaveChangesAsync();
        return res;
    }
    public async Task<List<Reservation>> GetReservationTermsByServiceId(int id, DateTime? date = null)
    {
        var reservations = await _databaseContext.Reservations
            .Where(r => r.OfferedService.Id == id)
            .ToListAsync();

        if (date != null)
        {
            reservations = reservations
                .Where(r => r.DateStart.Date == date.Value.Date)
                .ToList();
        }

        return reservations;
    }

    public async Task<List<Reservation>> GetReservationsByUserId(int id)
    {
        return await _databaseContext.Reservations
            .Include(r => r.OfferedService)
            .Include(r => r.OfferedService.TimeSlots)
            .Include(r => r.OfferedService.Location)
            .Include(r => r.User)
            .Where(r => r.User.Id == id).ToListAsync();
    }
    
    public async Task<Reservation> GetReservationById(int id)
    {
        Reservation? result;
        try
        {
            result = await _databaseContext.Reservations
                .Include(r => r.OfferedService)
                .Include(r => r.OfferedService.Owner)
                .Include(r => r.OfferedService.Location)
                .Include(r => r.User)
                .FirstAsync(r => r.Id == id);

        }
        catch (Exception)
        {
            throw new ResourceNotFoundException($"Reservation with id: '{id}' not found.");
        }

        return result;
    }
    
    public async void RemoveReservation(Reservation reservation)
    {
        _databaseContext.Reservations.Remove(reservation);
        await _databaseContext.SaveChangesAsync();
    }
}