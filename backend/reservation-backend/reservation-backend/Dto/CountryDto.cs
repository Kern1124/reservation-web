using System.Security.Cryptography;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using reservation_backend.Models;

namespace reservation_backend.Dto;

public class CountryDto
{
    [JsonProperty("country")]
    public string Name { get; set; }
    [JsonProperty("cities")]
    public string[] Cities { get; set; }
    public CountryDto(){}
    
    public CountryDto(Country country)
    {
        Name = country.Name;
        Cities = country.Cities.Select(c => c.Name).ToArray();
    }
}