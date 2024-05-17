using FastEndpoints;
using reservation_backend.Exceptions;
using reservation_backend.Features.Users.GetUserReservations;
using reservation_backend.Interfaces;
using reservation_backend.Dto;

namespace reservation_backend.Features.Reservations.GetMyReservations;

public class GetMyReservationsEndpoint : EndpointWithoutRequest<GetUserReservationsResponse>
{
    public IUserService UserService { get; set; }
    public IReservationService ReservationService { get; set; }

    public override void Configure()
    {
        Get("/api/reservations/my");
        Roles("logged-user");
        Options(x => x.WithTags("Reservations"));

    }
    public override async Task HandleAsync(CancellationToken ct)
    {
        int userId = int.Parse(HttpContext.User.Claims.First(c => c.Type == "id").Value);
        try
        {
            Response.Reservations = (await ReservationService
                    .GetReservationsByUserId(userId)).Select(r => new ReservationDto(r, r.OfferedService))
                .ToList();
        } catch (ResourceNotFoundException)
        {
            AddError("User not found");
            await SendErrorsAsync(404);
        }
        await SendOkAsync(Response, ct);
    }
}