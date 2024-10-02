using BeaniesUtilities.Models.Resume;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;

namespace Gay.TCazier.DatabaseParser.Models.Extensions;

public static class PersonExtensions
{
    public static void Create(this PersonModel model, int id, EditiblePersonModel props, string user)
    {
		model.PreferedName = props.PreferedName;
		model.Pronouns = props.Pronouns;
		model.Emails = props.Emails;
		model.Socials = props.Socials;
		model.Addresses = props.Addresses;
		model.PhoneNumbers = props.PhoneNumbers;

        model.Name = props.Name;
        model.IsHidden = props.IsHidden;

        model.CommonIdentity = id;
        model.CreatedBy = user;
        model.CreatedOn = DateTime.UtcNow;
        model.Notes = "Entry Creation";
        
    }

    public static PersonModel Update(this PersonModel model, EditiblePersonModel updates, string user)
    {
        bool previousHiddenState = model.IsHidden;
        model.IsHidden = true;

        return new PersonModel
        {
			PreferedName = updates.PreferedName,
			Pronouns = updates.Pronouns,
			Emails = updates.Emails,
			Socials = updates.Socials,
			Addresses = updates.Addresses,
			PhoneNumbers = updates.PhoneNumbers,

            Name = updates.Name,
            IsHidden = updates.IsHidden,

            CommonIdentity = model.CommonIdentity,
            CreatedBy = model.CreatedBy,
            CreatedOn = model.CreatedOn,
            IsDeleted = model.IsDeleted,

            ModifiedBy = user,
            ModifiedOn = DateTime.UtcNow,
            Notes = "Edited",
        };
    }
}