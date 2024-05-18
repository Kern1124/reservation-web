using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FastEndpoints;
using reservation_backend.Users;

namespace reservation_backend.Models;

public class OfferedService
{
    public int Id { get; set; }
    public User Owner { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Location Location { get; set; }
    public HashSet<TimeSlotSpan> TimeSlots { get; set; } = new ()
    {
        new("08:30", "09:00"),
        new("10:00", "10:30" ),
        new("11:00", "11:30"),
        new("12:00", "12:30")
    };
    public List<Reservation> Reservations { get; set; }
    public OfferedService(){}
    public OfferedService(string name, string description){}
    
    public OfferedService(string name, string description, Location location)
    {
        Name = name;
        Description = description;
        Location = location;
    }
    public OfferedService(User owner, string name, string description, Location location)
    {
        Owner = owner;
        Name = name;
        Description = description;
        Location = location;
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