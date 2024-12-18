using BeaniesUtilities.Models.Resume;
using FluentValidation;

namespace Gay.TCazier.Resume.BLL.Validators.V1;

#pragma warning disable CS1591

public class PhoneNumberModelValidator : AbstractValidator<PhoneNumberModel>
{
    public PhoneNumberModelValidator()
    {
		RuleFor(x => x.CountryCode).NotEmpty().WithMessage("The CountryCode Property of PhoneNumberModel cannot be empty");
		RuleFor(x => x.AreaCode).NotEmpty().WithMessage("The AreaCode Property of PhoneNumberModel cannot be empty");
		RuleFor(x => x.TelephonePrefix).NotEmpty().WithMessage("The TelephonePrefix Property of PhoneNumberModel cannot be empty");
		RuleFor(x => x.LineNumber).NotEmpty().WithMessage("The LineNumber Property of PhoneNumberModel cannot be empty");
    }
}

#pragma warning restore CS1591