using reservation_backend.Dto;

namespace reservation_backend.Features.Countries;

public class GetCountriesResponse
{
    public List<CountryDto> Countries { get; set; }
}