using FastEndpoints;
using FluentValidation;
using reservation_backend.Features.OfferedServices.SharedRequestBodies;

namespace reservation_backend.Features.OfferedServices.Validators;

public class ThumbnailImageValidator : Validator<IThumbnailImageRequest>
{
    const int MaxImageSize = 2 * 1024 * 1024;

    public ThumbnailImageValidator()
    {
        RuleFor(x => x.Image)
            .NotEmpty()
            .WithMessage("Image is a required field!");

        RuleFor(x => x.Image.Length)
            .LessThan(MaxImageSize)
            .WithMessage($"Maximum image size is {MaxImageSize / (1024 * 1024)}MB");
    }
}