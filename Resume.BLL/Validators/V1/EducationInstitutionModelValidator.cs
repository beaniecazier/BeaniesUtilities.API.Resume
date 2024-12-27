using BeaniesUtilities.Models.Resume;
using FluentValidation;

namespace Gay.TCazier.Resume.BLL.Validators.V1;

#pragma warning disable CS1591

public class EducationInstitutionModelValidator : AbstractValidator<EducationInstitutionModel>
{
    public EducationInstitutionModelValidator()
    {
		//RuleFor(x => x.CertificatesIssued).NotEmpty().WithMessage("The CertificatesIssued Property of EducationInstitutionModel cannot be empty");
		//RuleFor(x => x.DegreesGiven).NotEmpty().WithMessage("The DegreesGiven Property of EducationInstitutionModel cannot be empty");
    }
}

#pragma warning restore CS1591