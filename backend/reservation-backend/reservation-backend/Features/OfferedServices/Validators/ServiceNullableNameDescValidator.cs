using FastEndpoints;
using FluentValidation;
using reservation_backend.Features.OfferedServices.SharedRequestBodies;

namespace reservation_backend.Features.OfferedServices.Validators;

public class ServiceNullableNameDescValidator : Validator<IServiceNameDescRequest>
{
    public ServiceNullableNameDescValidator()
    {
        RuleFor(x => x.Name)
            .MinimumLength(5)
            .WithMessage("Service name is too short!");
        
        RuleFor(x => x.Description)
            .MinimumLength(24)
            .WithMessage("The description is too short! Use at least 24 characters!");
    }
}