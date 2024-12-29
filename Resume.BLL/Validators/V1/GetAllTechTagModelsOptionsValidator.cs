using FluentValidation;
using Gay.TCazier.Resume.BLL.Options.V1;

namespace Resume.BLL;

#pragma warning disable CS1591

public class GetAllTechTagModelsOptionsValidator : AbstractValidator<GetAllTechTagModelsOptions>
{
    private static readonly string[] AcceptableSortFields =
    {
        "Name", "ModifiedBy", "ModifiedOn", "HiddenOn", "DeletedOn", "EntryIdentity", "CommonIdentity"
    };

    public GetAllTechTagModelsOptionsValidator()
    {
        RuleFor(x => x.GreaterThanOrEqualToID).GreaterThanOrEqualTo(0).When(x=>x.GreaterThanOrEqualToID.HasValue);
        RuleFor(x => x.LessThanOrEqualToID).GreaterThanOrEqualTo(0).When(x => x.LessThanOrEqualToID.HasValue);

        RuleFor(x => x.SortField).Must(x => x is null || AcceptableSortFields.Contains(x))
            .WithMessage($"The only acceptable sorting fields are:\n{string.Join("\n", AcceptableSortFields)}");
    }
}

#pragma warning restore CS1591