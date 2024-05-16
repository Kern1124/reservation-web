using FastEndpoints;
using reservation_backend.Dto;
using reservation_backend.Interfaces;
using reservation_backend.Services;

namespace reservation_backend.Features.Countries;

public class GetAllCountriesEndpoint : EndpointWithoutRequest<GetCountriesResponse>
{
    public ILocationService LocationService { get; set; }
    public override void Configure()
    {
        Get("/api/countries");
        AllowAnonymous();
        Options(x => x.WithTags("Countries"));
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        Response.Countries = LocationService.GetAllCountries().Select(c => new CountryDto(c)).ToList();
        await SendOkAsync(Response, ct);}
    }
    