using BeaniesUtilities.Models.Resume;
using FluentValidation;

namespace Gay.TCazier.Resume.BLL.Validators.V1;

#pragma warning disable CS1591

public class PersonModelValidator : AbstractValidator<PersonModel>
{
    public PersonModelValidator()
    {
		RuleFor(x => x.Pronouns).NotEmpty().WithMessage("The Pronouns Property of PersonModel cannot be empty");
		RuleFor(x => x.Emails).NotEmpty().WithMessage("The Emails Property of PersonModel cannot be empty");
		RuleFor(x => x.Socials).NotEmpty().WithMessage("The Socials Property of PersonModel cannot be empty");
		RuleFor(x => x.Addresses).NotEmpty().WithMessage("The Addresses Property of PersonModel cannot be empty");
		RuleFor(x => x.PhoneNumbers).NotEmpty().WithMessage("The PhoneNumbers Property of PersonModel cannot be empty");
    }
}

#pragma warning restore CS1591