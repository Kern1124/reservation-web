using reservation_backend.Dto;

namespace reservation_backend.Models;

public class Country
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<City> Cities { get; set; }
    
    public Country(CountryDto dto)
    {
        Name = dto.Name;
        Cities = new List<City>();
        foreach (var city in dto.Cities)
        {
            Cities.Add(new City(city));
        }
    }
    public Country(string name)
    {
        Name = name;
    }
}