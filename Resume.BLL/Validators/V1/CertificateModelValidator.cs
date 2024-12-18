using BeaniesUtilities.Models.Resume;
using FluentValidation;

namespace Gay.TCazier.Resume.BLL.Validators.V1;

#pragma warning disable CS1591

public class CertificateModelValidator : AbstractValidator<CertificateModel>
{
    public CertificateModelValidator()
    {
		RuleFor(x => x.IssueDate).NotEmpty().WithMessage("The IssueDate Property of CertificateModel cannot be empty");
		RuleFor(x => x.Issuer).NotEmpty().WithMessage("The Issuer Property of CertificateModel cannot be empty");
    }
}

#pragma warning restore CS1591