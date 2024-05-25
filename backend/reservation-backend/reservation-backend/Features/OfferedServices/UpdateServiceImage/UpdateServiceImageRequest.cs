using reservation_backend.Features.OfferedServices.SharedRequestBodies;

namespace reservation_backend.Features.OfferedServices.UpdateServiceImage;

public class UpdateServiceImageRequest : IThumbnailImageRequest
{
    public int Id { get; set; }
    public IFormFile Image { get; set; }
}