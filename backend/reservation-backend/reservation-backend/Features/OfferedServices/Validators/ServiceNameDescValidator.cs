using FastEndpoints;
using FluentValidation;
using Namotion.Reflection;
using reservation_backend.Features.OfferedServices.CreateService;
using reservation_backend.Features.OfferedServices.SharedRequestBodies;
using reservation_backend.Features.OfferedServices.UpdateService;

namespace reservation_backend.Features.OfferedServices.Validators;

public class ServiceNameDescValidator : ServiceNullableNameDescValidator
{
    public ServiceNameDescValidator() : base()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("The service name cannot be empty!");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("The service description cannot be empty!");
    }
}