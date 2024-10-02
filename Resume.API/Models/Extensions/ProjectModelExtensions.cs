using BeaniesUtilities.Models.Resume;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;

namespace Gay.TCazier.DatabaseParser.Models.Extensions;

public static class ProjectExtensions
{
    public static void Create(this ProjectModel model, int id, EditibleProjectModel props, string user)
    {
		model.Description = props.Description;
		model.Version = props.Version;
		model.ProjectUrl = props.ProjectUrl;
		model.StartDate = props.StartDate;
		model.CompletionDate = props.CompletionDate;
		model.TechTags = props.TechTags;

        model.Name = props.Name;
        model.IsHidden = props.IsHidden;

        model.CommonIdentity = id;
        model.CreatedBy = user;
        model.CreatedOn = DateTime.UtcNow;
        model.Notes = "Entry Creation";
        
    }

    public static ProjectModel Update(this ProjectModel model, EditibleProjectModel updates, string user)
    {
        bool previousHiddenState = model.IsHidden;
        model.IsHidden = true;

        return new ProjectModel
        {
			Description = updates.Description,
			Version = updates.Version,
			ProjectUrl = updates.ProjectUrl,
			StartDate = updates.StartDate,
			CompletionDate = updates.CompletionDate,
			TechTags = updates.TechTags,

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