namespace reservation_backend.Models;

public class Location
{
    public int Id { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Address { get; set; }

    public Location(string country, string city, string address)
    {
        Country = country;
        City = city;
        Address = address;
    }
}