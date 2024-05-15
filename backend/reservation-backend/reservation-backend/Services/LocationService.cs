using Microsoft.EntityFrameworkCore;
using reservation_backend.Database;
using reservation_backend.Interfaces;
using reservation_backend.Models;

namespace reservation_backend.Services;

public class LocationService : ILocationService
{
    private Context _databaseContext;
    public LocationService(Context databaseContext)
    {
        _databaseContext = databaseContext;
    }
    
    public bool CheckIfCityBelongsToCountry(string name, string country)
    {
        var city = _databaseContext.Cities.FirstOrDefault(c => c.Name == name);
        if (city == null)
        {
            return false;
        }
        return _databaseContext.Countries.Any(c => c.Name == country && c.Cities.Select(ct => ct.Name == name).Any());
    }

    public List<Country> GetAllCountries()
    {
        return _databaseContext.Countries.Include(c => c.Cities).ToList();
    }
}