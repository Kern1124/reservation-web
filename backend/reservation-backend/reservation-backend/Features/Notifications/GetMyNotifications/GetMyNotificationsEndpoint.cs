using FastEndpoints;
using reservation_backend.Dto;
using reservation_backend.Exceptions;
using reservation_backend.Interfaces;
using reservation_backend.Models;

namespace reservation_backend.Features.Notifications.GetMyNotifications;

public class GetMyNotificationsEndpoint : Endpoint<GetMyNotificationsRequest, GetMyNotificationsResponse>
{
    public INotificationService NotificationService { get; set; }
    public override void Configure()
    {
        Get("/api/notifications/my");
        Options(x => x.WithTags("Notifications"));
    }
    
    public override async Task HandleAsync(GetMyNotificationsRequest req, CancellationToken ct)
    {
        int userId = int.Parse(HttpContext.User.Claims.First(c => c.Type == "id").Value);
        List<Notification>? notifications = null;
        try
        {
            notifications = await NotificationService.GetLastUserNotifications(userId, 10);
        } catch (ResourceNotFoundException)
        {
            AddError("User not found");
            await SendErrorsAsync(404);
            return;
        }

        Response.Notifications = notifications!.Select(n => new NotificationDto(n)).ToList();
        await SendOkAsync(Response, ct);
    }
}