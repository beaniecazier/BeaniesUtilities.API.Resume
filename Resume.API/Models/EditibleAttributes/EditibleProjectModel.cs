using BeaniesUtilities.Models.Resume;

namespace Gay.TCazier.DatabaseParser.Models.EditibleAttributes;

public class EditibleProjectModel : EditibleBaseModel
{
    public string? Description { get; set; }
    public string? Version { get; set; }
    public string? ProjectUrl { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; }
    public DateTime? CompletionDate { get; set; }

    public List<TechTagModel> TechTags { get; set; } = new List<TechTagModel>();
}