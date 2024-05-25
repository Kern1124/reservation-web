using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FastEndpoints;
using reservation_backend.Users;

namespace reservation_backend.Models;

public class OfferedService
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? ImageUri { get; set; }
    public User Owner { get; set; }
    public Location Location { get; set; }
    public List<TimeSlotSpan> TimeSlots { get; set; }
    public List<Reservation> Reservations { get; set; }
    public OfferedService(){}
    public OfferedService(string name, string description){}
    
    public OfferedService(string name, string description, Location location)
    {
        Name = name;
        Description = description;
        Location = location;
    }
    public OfferedService(User owner, string name, string description, Location location, List<TimeSlotSpan> timeSlots)
    {
        Owner = owner;
        Name = name;
        Description = description;
        Location = location;
        TimeSlots = timeSlots;
    }
    public OfferedService(int id, User owner, string name, string description, Location location)
    {
        Id = id;
        Owner = owner;
        Name = name;
        Description = description;
        Location = location;
    }
}