using FastEndpoints;
using reservation_backend.Dto;
using reservation_backend.Interfaces;

namespace reservation_backend.Features.Users.GetUserReservations;

public class GetUserReservationsEndpoint : Endpoint<GetUserReservationsRequest, GetUserReservationsResponse>
{
    
    public IUserService UserService { get; set; }

    public override void Configure()
    {
        Get("/api/users/{id}/reservations");
        Roles("logged-user");
    }
    public override async Task HandleAsync(GetUserReservationsRequest req, CancellationToken ct)
    {
        var user = UserService.GetUserById(req.Id);
        if (user == null)
        {
            AddError("User not found");
            await SendErrorsAsync();
        }
        Response.Reservations = user!.Reservations
            .Select(r => new ReservationDto(r, r.OfferedService))
            .ToList();
    }
}