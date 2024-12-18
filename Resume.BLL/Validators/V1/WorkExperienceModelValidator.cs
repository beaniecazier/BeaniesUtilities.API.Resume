using BeaniesUtilities.Models.Resume;
using FluentValidation;

namespace Gay.TCazier.Resume.BLL.Validators.V1;

#pragma warning disable CS1591

public class WorkExperienceModelValidator : AbstractValidator<WorkExperienceModel>
{
    public WorkExperienceModelValidator()
    {
		RuleFor(x => x.StartDate).NotEmpty().WithMessage("The StartDate Property of WorkExperienceModel cannot be empty");
		RuleFor(x => x.EndDate).NotEmpty().WithMessage("The EndDate Property of WorkExperienceModel cannot be empty");
		RuleFor(x => x.Responsibilities).NotEmpty().WithMessage("The Responsibilities Property of WorkExperienceModel cannot be empty");
		RuleFor(x => x.TechUsed).NotEmpty().WithMessage("The TechUsed Property of WorkExperienceModel cannot be empty");
    }
}

#pragma warning restore CS1591