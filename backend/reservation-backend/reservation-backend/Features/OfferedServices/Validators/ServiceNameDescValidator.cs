using FastEndpoints;
using FluentValidation;
using Namotion.Reflection;
using reservation_backend.Features.OfferedServices.CreateService;
using reservation_backend.Features.OfferedServices.SharedRequestBodies;
using reservation_backend.Features.OfferedServices.UpdateService;

namespace reservation_backend.Features.OfferedServices.Validators;

public class ServiceNameDescValidator : ServiceNullableNameDescValidator
{
    const int MaximumServiceNameLength = 24;
    const int MaximumServiceDescriptionLength = 256;
    public ServiceNameDescValidator() : base()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(MaximumServiceNameLength)
            .WithMessage("The service name cannot be empty!");

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(MaximumServiceDescriptionLength)
            .WithMessage("The service description cannot be empty!");
    }
}