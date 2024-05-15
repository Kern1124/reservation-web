using FastEndpoints;
using FluentValidation;
using reservation_backend.Features.OfferedServices.SharedRequestBodies;

namespace reservation_backend.Features.OfferedServices.Validators;

public class ServiceLocationValidator : Validator<IServiceLocationRequest>
{
    public ServiceLocationValidator()
    {
        RuleFor(x => x.Location.City)
            .NotEmpty()
            .WithMessage("We need service city!");

        RuleFor(x => x.Location.Country)
            .NotEmpty()
            .WithMessage("We need service country!");
        
        RuleFor(x => x.Location.Address)
            .NotEmpty()
            .WithMessage("We need service address!")
            .MinimumLength(5)
            .WithMessage("Address is too short!");
    }
}