using FastEndpoints;
using reservation_backend.Exceptions;
using reservation_backend.Features.OfferedServices.UpdateServiceImage;
using reservation_backend.Interfaces;

namespace reservation_backend.Features.OfferedServices;

public class UpdateServiceImageEndpoint : Endpoint<UpdateServiceImageRequest>
{
    public IOSService OSService { get; set; }

    public override void Configure()
    {
        Put("/api/services/{id}/image");
        Roles("logged-user");
        AllowFileUploads();
        Options(x => x.WithTags("OfferedServices"));
    }

    public override async Task HandleAsync(UpdateServiceImageRequest req, CancellationToken ct)
    {
        try
        {
            var service = await OSService.GetServiceById(req.Id);
            if (service.Owner.Id != int.Parse(HttpContext.User.Claims.First(c => c.Type == "id").Value))
            {
                AddError("You don't have permission to update this service");
                await SendErrorsAsync(403);
                return;
            }
            await OSService.UpdateServiceImage(req.Id, req.Image);
        }
        catch (ResourceNotFoundException)
        {
            AddError("Service not found");
            await SendErrorsAsync(404);
            return;
        }
        catch (Exception)
        {
            AddError("Could not update image");
            await SendErrorsAsync(500);
            return;
        }

        await SendOkAsync();
    }
    
}