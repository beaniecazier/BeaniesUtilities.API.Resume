using BeaniesUtilities.Models.Resume;

namespace Gay.TCazier.DatabaseParser.Models.EditibleAttributes;

public class EditibleWorkExperienceModel : EditibleBaseModel
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Company { get; set; }
    public string? Description { get; set; }
    public List<string> Responsibilities { get; set; } = new List<string>();
    public List<TechTagModel> TechUsed { get; set; } = new List<TechTagModel>();
}