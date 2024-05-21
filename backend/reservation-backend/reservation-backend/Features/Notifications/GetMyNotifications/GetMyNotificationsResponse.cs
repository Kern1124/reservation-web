using reservation_backend.Dto;

namespace reservation_backend.Features.Notifications.GetMyNotifications;

public class GetMyNotificationsResponse
{
    public List<NotificationDto> Notifications { get; set; }
}