using FluentValidation;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;

namespace Gay.TCazier.DatabaseParser.Validators;

public class EduInstituteModelVaildator : AbstractValidator<EditibleEduInstituteModel>
{
    public EduInstituteModelVaildator()
    {
    }
}
