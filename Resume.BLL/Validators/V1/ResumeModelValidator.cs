using BeaniesUtilities.Models.Resume;
using FluentValidation;

namespace Gay.TCazier.Resume.BLL.Validators.V1;

#pragma warning disable CS1591

public class ResumeModelValidator : AbstractValidator<ResumeModel>
{
    public ResumeModelValidator()
    {
		RuleFor(x => x.Degrees).NotEmpty().WithMessage("The Degrees Property of ResumeModel cannot be empty");
		RuleFor(x => x.Certificates).NotEmpty().WithMessage("The Certificates Property of ResumeModel cannot be empty");
		RuleFor(x => x.WorkExperience).NotEmpty().WithMessage("The WorkExperience Property of ResumeModel cannot be empty");
		RuleFor(x => x.Projects).NotEmpty().WithMessage("The Projects Property of ResumeModel cannot be empty");
		RuleFor(x => x.Pronouns).NotEmpty().WithMessage("The Pronouns Property of ResumeModel cannot be empty");
		RuleFor(x => x.Emails).NotEmpty().WithMessage("The Emails Property of ResumeModel cannot be empty");
		RuleFor(x => x.Socials).NotEmpty().WithMessage("The Socials Property of ResumeModel cannot be empty");
		RuleFor(x => x.Addresses).NotEmpty().WithMessage("The Addresses Property of ResumeModel cannot be empty");
		RuleFor(x => x.PhoneNumbers).NotEmpty().WithMessage("The PhoneNumbers Property of ResumeModel cannot be empty");
    }
}

#pragma warning restore CS1591