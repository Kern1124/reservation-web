using FastEndpoints;
using reservation_backend.Dto;
using reservation_backend.Exceptions;
using reservation_backend.Interfaces;
using reservation_backend.Models;

namespace reservation_backend.Features.Users.GetUserReservations;

public class GetUserReservationsEndpoint : Endpoint<GetUserReservationsRequest, GetUserReservationsResponse>
{
    
    public IUserService UserService { get; set; }

    public override void Configure()
    {
        Get("/api/users/{id}/reservations");
        Roles("logged-user");
        Options(x => x.WithTags("Users"));
    }
    public override async Task HandleAsync(GetUserReservationsRequest req, CancellationToken ct)
    {
        User user;
        try
        {
            user = await UserService.GetUserById(req.Id);
        }
        catch (ResourceNotFoundException)
        {
            AddError("User not found");
            await SendErrorsAsync(404);
            return;
        }

        Response.Reservations = (await UserService.GetUserReservations(user!.Id))
            .Select(r => new ReservationDto(r, r.OfferedService))
            .ToList();
    }
}