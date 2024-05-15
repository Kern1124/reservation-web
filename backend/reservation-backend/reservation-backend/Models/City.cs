using reservation_backend.Dto;

namespace reservation_backend.Models;

public class City
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public City (string name)
    {
        Name = name;
    }
    public City(CityDto dto)
    {
        Name = dto.Name;
    }
}