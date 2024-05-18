using FastEndpoints;
using reservation_backend.Dto;
using reservation_backend.Exceptions;
using reservation_backend.Features.Users.GetUserReservations;
using reservation_backend.Interfaces;
using reservation_backend.Models;

namespace reservation_backend.Features.OfferedServices.GetServiceReservations;

public class GetServiceReservationsEndpoint : Endpoint<GetServiceReservationsRequest, GetServiceReservationsResponse>
{
    public IOSService OSService { get; set; }
    public override void Configure()
    {
        Get("/api/services/{id}/reservations");
        AllowAnonymous();
        Options(x => x.WithTags("OfferedServices"));
    }
    
    public override async Task HandleAsync(GetServiceReservationsRequest req, CancellationToken ct)
    {
        int id = Route<int>("id");
        Response.Reservations = (await OSService.GetServiceReservations(id))
            .Select(r => new ReservationDto(r))
            .ToList();
        await SendOkAsync(Response, ct);
    }
    
}