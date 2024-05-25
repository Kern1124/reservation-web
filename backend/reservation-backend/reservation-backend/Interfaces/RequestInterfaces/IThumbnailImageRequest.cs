namespace reservation_backend.Features.OfferedServices.SharedRequestBodies;

public interface IThumbnailImageRequest
{
    IFormFile Image { get; set; }
}