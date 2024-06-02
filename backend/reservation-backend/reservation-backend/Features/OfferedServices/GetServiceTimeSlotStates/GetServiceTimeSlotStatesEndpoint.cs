using FastEndpoints;
using reservation_backend.Exceptions;
using reservation_backend.Features.OfferedServices.GetServiceTimeSlots;
using reservation_backend.Interfaces;
using reservation_backend.Models;

namespace reservation_backend.Features.OfferedServices.GetServiceTimeSlotStates;

public class GetServiceTimeSlotStatesEndpoint : Endpoint<GetTimeSlotStatesRequest, GetTimeSlotStatesResponse>
{
    public IOSService OSService { get; set; }
    public override void Configure()
    {
        Get("/api/services/{id}/time-slots/{date}");
        Options(x => x.WithTags("OfferedServices"));

    }

    public override async Task HandleAsync(GetTimeSlotStatesRequest req, CancellationToken ct)
    {
        try
        {
            Response.TimeSlots = await OSService.GetTimeSlotsByServiceIdAndDate(req.Id, req.Date);
        }
        catch (ResourceNotFoundException)
        {
            AddError("Service not found");
            await SendErrorsAsync(404);
            return;
        }
        await SendOkAsync(Response, ct);
        
    }
}