using BeaniesUtilities.Models.Resume;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;

namespace Gay.TCazier.DatabaseParser.Models.Extensions;

public static class WorkExperienceExtensions
{
    public static void Create(this WorkExperienceModel model, int id, EditibleWorkExperienceModel props, string user)
    {
		model.StartDate = props.StartDate;
		model.EndDate = props.EndDate;
		model.Company = props.Company;
		model.Description = props.Description;
		model.Responsibilities = props.Responsibilities;
		model.TechUsed = props.TechUsed;

        model.Name = props.Name;
        model.IsHidden = props.IsHidden;

        model.CommonIdentity = id;
        model.CreatedBy = user;
        model.CreatedOn = DateTime.UtcNow;
        model.Notes = "Entry Creation";
        
    }

    public static WorkExperienceModel Update(this WorkExperienceModel model, EditibleWorkExperienceModel updates, string user)
    {
        bool previousHiddenState = model.IsHidden;
        model.IsHidden = true;

        return new WorkExperienceModel
        {
			StartDate = updates.StartDate,
			EndDate = updates.EndDate,
			Company = updates.Company,
			Description = updates.Description,
			Responsibilities = updates.Responsibilities,
			TechUsed = updates.TechUsed,

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