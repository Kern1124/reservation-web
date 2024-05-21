using FastEndpoints;
using reservation_backend.Exceptions;
using reservation_backend.Interfaces;
using reservation_backend.Models;

namespace reservation_backend.Features.Notifications;

public class SetNotificationAsReadEndpoint : Endpoint<SetNotificationAsReadRequest, SetNotificationAsReadResponse>
{
    public INotificationService NotificationService { get; set; }
    public override void Configure()
    {
        Post("/api/notifications/{id}/setAsRead");
        Options(x => x.WithTags("Notifications"));
        Roles("logged-user");
    }
    
    public override async Task HandleAsync(SetNotificationAsReadRequest req, CancellationToken ct)
    {
        Notification notification;
        try
        {
             notification = await NotificationService.GetNotificationById(req.Id);
        }
        catch (ResourceNotFoundException)
        {
            AddError("Notification not found");
            await SendErrorsAsync();
            return;
        }

        if (notification.Recipient.Id != int.Parse(HttpContext.User.Claims.First(c => c.Type == "id").Value))
        {
            AddError("Unauthorized access");
            await SendErrorsAsync(403);
            return;
        }
        await NotificationService.SetAsRead(req.Id);
        Response.Message = "Notification set as read";
    }
}