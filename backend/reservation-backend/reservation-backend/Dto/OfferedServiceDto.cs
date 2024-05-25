namespace reservation_backend.Models;

public class OfferedServiceDto
{
    public int Id { get; set; }
    public UserDto Owner { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? ImageFileName { get; set; }
    public LocationDto Location { get; set; }
    public List<TimeSlotDto> TimeSlots { get; set; }
    public OfferedServiceDto(OfferedService service)
    {
        Id = service.Id;
        Owner = new UserDto(service.Owner);
        Name = service.Name;
        Description = service.Description;
        Location = new LocationDto(service.Location);
        TimeSlots = service.TimeSlots
            .Select(t => new TimeSlotDto(t))
            .ToList();
        ImageFileName = service.ImageUri?.Split("/").Last();
    }
}