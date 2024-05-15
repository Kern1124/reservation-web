using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace reservation_backend.Dto;

public class CountriesDto
{
    [JsonProperty("data")] 
    public CountryDto[] Data { get; set; }
}