using FluentValidation;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;

namespace Gay.TCazier.DatabaseParser.Validators;

public class ResumeModelVaildator : AbstractValidator<EditibleResumeModel>
{
    public ResumeModelVaildator()
    {
    }
}
