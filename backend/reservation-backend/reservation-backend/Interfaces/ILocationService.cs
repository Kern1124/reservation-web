using reservation_backend.Models;

namespace reservation_backend.Interfaces;

public interface ILocationService
{
    public List<Country> GetAllCountries();
    public bool CheckIfCityBelongsToCountry(string name, string country);
}