using BeaniesUtilities.Models.Resume;

namespace Gay.TCazier.DatabaseParser.Models.EditibleAttributes;

public class EditibleResumeModel : EditiblePersonModel
{
    public string? HeroStatement { get; set; }

    public List<EducationDegreeModel> Degrees { get; set; } = new List<EducationDegreeModel>();
    public List<CertificateModel> Certificates { get; set; } = new List<CertificateModel>();
    public List<WorkExperienceModel> WorkExperience { get; set; } = new List<WorkExperienceModel>();
    public List<ProjectModel> Projects { get; set; } = new List<ProjectModel>();
}
