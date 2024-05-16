using FastEndpoints;
using reservation_backend.Interfaces;

namespace reservation_backend.Features.OfferedServices.GetServiceTimeSlots;

public class GetServiceTimeSlotsEndpoint : Endpoint<TimeSlotRequest, TimeSlotResponse>
{
    public IOSService _osService { get; set; }
    public override void Configure()
    {
        Get("/api/services/{serviceId}/time-slots/{date}");
        Options(x => x.WithTags("OfferedServices"));

    }

    public override async Task HandleAsync(TimeSlotRequest req, CancellationToken ct)
    {
        var service = _osService.GetServiceById(req.ServiceId);
        if (service == null)
        {
            await SendNotFoundAsync(ct);
        }
        else
        {
            Response.TimeSlots = _osService.GetTimeSlotsByServiceIdAndDate(req.ServiceId, req.Date);
            await SendOkAsync(Response, ct);
        }
    }
}