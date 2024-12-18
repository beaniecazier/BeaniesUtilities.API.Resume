using BeaniesUtilities.Models.Resume;
using FluentValidation;

namespace Gay.TCazier.Resume.BLL.Validators.V1;

#pragma warning disable CS1591

public class ProjectModelValidator : AbstractValidator<ProjectModel>
{
    public ProjectModelValidator()
    {
		RuleFor(x => x.TechTags).NotEmpty().WithMessage("The TechTags Property of ProjectModel cannot be empty");
    }
}

#pragma warning restore CS1591