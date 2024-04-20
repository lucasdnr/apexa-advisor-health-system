
using AdvisorHealthAPI.Requests;
using FluentValidation;

namespace AdvisorHealthAPI.Validators;

public class AdvisorValidator :  AbstractValidator<AdvisorRequest>
{
    public AdvisorValidator()
    {
        // Name
        RuleFor(p => p.Name)
            .MaximumLength(255)
            .NotEmpty();
        
        // Sin Number
        RuleFor(p => p.SinNumber)
            .NotEmpty()
            .Must(w => w.ToString().Length == 9)
            .WithMessage("Must be a size 9");
        
        // Address
        RuleFor(p => p.Address)
            .MaximumLength(255);
        
        // Phone
        RuleFor(p => p.Phone)
            .Must(w => w?.ToString().Length == 8)
            .WithMessage("Must be a size 8")
            .When(x => !string.IsNullOrEmpty(x.Phone.ToString()));
    }
}
