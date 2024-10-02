using BeaniesUtilities.Models.Resume;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;

namespace Gay.TCazier.DatabaseParser.Models.Extensions;

public static class EducationDegreeExtensions
{
    public static void Create(this EducationDegreeModel model, int id, EditibleEducationDegreeModel props, string user)
    {
		model.GPA = props.GPA;
		model.StartDate = props.StartDate;
		model.EndDate = props.EndDate;
		model.Institution = props.Institution;

        model.Name = props.Name;
        model.IsHidden = props.IsHidden;

        model.CommonIdentity = id;
        model.CreatedBy = user;
        model.CreatedOn = DateTime.UtcNow;
        model.Notes = "Entry Creation";
        
    }

    public static EducationDegreeModel Update(this EducationDegreeModel model, EditibleEducationDegreeModel updates, string user)
    {
        bool previousHiddenState = model.IsHidden;
        model.IsHidden = true;

        return new EducationDegreeModel
        {
			GPA = updates.GPA,
			StartDate = updates.StartDate,
			EndDate = updates.EndDate,
			Institution = updates.Institution,

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