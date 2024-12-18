using FluentValidation;
using Gay.TCazier.Resume.BLL.Options.V1;

namespace Resume.BLL;

public class GetAllResumeModelsOptionsValidator : AbstractValidator<GetAllResumeModelsOptions>
{
    public GetAllResumeModelsOptionsValidator()
    {
        RuleFor(x => x.GreaterThanOrEqualToID).GreaterThanOrEqualTo(0).When(x=>x.GreaterThanOrEqualToID.HasValue);
        RuleFor(x => x.LessThanOrEqualToID).GreaterThanOrEqualTo(0).When(x => x.LessThanOrEqualToID.HasValue);
    }
}

#pragma warning restore CS1591