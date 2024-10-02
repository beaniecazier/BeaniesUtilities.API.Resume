using BeaniesUtilities.Models.Resume;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;

namespace Gay.TCazier.DatabaseParser.Models.Extensions;

public static class ResumeExtensions
{
    public static void Create(this ResumeModel model, int id, EditibleResumeModel props, string user)
    {
		model.HeroStatement = props.HeroStatement;
		model.Degrees = props.Degrees;
		model.Certificates = props.Certificates;
		model.WorkExperience = props.WorkExperience;
		model.Projects = props.Projects;
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

    public static ResumeModel Update(this ResumeModel model, EditibleResumeModel updates, string user)
    {
        bool previousHiddenState = model.IsHidden;
        model.IsHidden = true;

        return new ResumeModel
        {
			HeroStatement = updates.HeroStatement,
			Degrees = updates.Degrees,
			Certificates = updates.Certificates,
			WorkExperience = updates.WorkExperience,
			Projects = updates.Projects,
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