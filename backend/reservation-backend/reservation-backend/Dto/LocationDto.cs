namespace reservation_backend.Models;

public class LocationDto
{
    public string Country { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public LocationDto(){}
    public LocationDto(string country, string city, string address){}
    public LocationDto(Location location)
    {
        Country = location.Country;
        City = location.City;
        Address = location.Address;
    }
}