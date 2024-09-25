using FluentValidation;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;

namespace Gay.TCazier.DatabaseParser.Validators;

public class PersonModelVaildator : AbstractValidator<EditiblePersonModel>
{
    public PersonModelVaildator()
    {
    }
}
