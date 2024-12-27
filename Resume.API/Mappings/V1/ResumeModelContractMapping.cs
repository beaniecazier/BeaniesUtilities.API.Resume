using BeaniesUtilities.Models.Resume;
using Gay.TCazier.Resume.BLL.Options.V1;
using Gay.TCazier.Resume.Contracts.Requests.V1.Create;
using Gay.TCazier.Resume.Contracts.Requests.V1.GetAll;
using Gay.TCazier.Resume.Contracts.Requests.V1.Update;
using Gay.TCazier.Resume.Contracts.Responses.V1;

namespace Gay.TCazier.Resume.API.Mappings.V1;

#pragma warning disable CS1591

public static class ResumeModelContractMapping
{
    public static ResumeModel MapToModelFromCreateRequest(this CreateResumeModelRequest request,
        int id, string username, List<EducationDegreeModel> degreeses, List<CertificateModel> certificateses, List<WorkExperienceModel> workExperiences, List<ProjectModel> projectses, List<AddressModel> addresseses, List<PhoneNumberModel> phoneNumberses)
    {
        return new ResumeModel(id, request.Name, username, request.Notes)
        {
			HeroStatement = request.HeroStatement,
			Degrees = degreeses,
			Certificates = certificateses,
			WorkExperience = workExperiences,
			Projects = projectses,
			PreferedName = request.PreferedName,
			Pronouns = request.Pronouns,
			Emails = request.Emails,
			Socials = request.Socials,
			Addresses = addresseses,
			PhoneNumbers = phoneNumberses,
        };
    }

    public static ResumeModel MapToModelFromUpdateRequest(this UpdateResumeModelRequest request, ResumeModel model,
        string username, List<EducationDegreeModel> degreeses, List<CertificateModel> certificateses, List<WorkExperienceModel> workExperiences, List<ProjectModel> projectses, List<AddressModel> addresseses, List<PhoneNumberModel> phoneNumberses)
    {
        string name = request.Name is null ? model.Name : request.Name;
        return new ResumeModel(request.Id, name, username, request.Notes)
        {
			HeroStatement = string.IsNullOrWhiteSpace(request.HeroStatement) ? model.HeroStatement : request.HeroStatement,
			Degrees = request.Degrees.Count() <= 0 ? model.Degrees : degreeses,
			Certificates = request.Certificates.Count() <= 0 ? model.Certificates : certificateses,
			WorkExperience = request.WorkExperience.Count() <= 0 ? model.WorkExperience : workExperiences,
			Projects = request.Projects.Count() <= 0 ? model.Projects : projectses,
			PreferedName = string.IsNullOrWhiteSpace(request.PreferedName) ? model.PreferedName : request.PreferedName,
			Pronouns = request.Pronouns.Count() <= 0 ? model.Pronouns : request.Pronouns,
			Emails = request.Emails.Count() <= 0 ? model.Emails : request.Emails,
			Socials = request.Socials.Count() <= 0 ? model.Socials : request.Socials,
			Addresses = request.Addresses.Count() <= 0 ? model.Addresses : addresseses,
			PhoneNumbers = request.PhoneNumbers.Count() <= 0 ? model.PhoneNumbers : phoneNumberses,
        };
    }

    public static GetAllResumeModelsOptions MapToOptions(this GetAllResumeModelsRequest options)
    {
        return new GetAllResumeModelsOptions()
        {
            NameSearchTerm = options.NameSearchTerm,
            NotesSearchTerm = options.NotesSearchTerm,
            AfterDate = options.AfterDate,
            BeforeDate = options.BeforeDate,
            AllowHidden = options.AllowHidden,
            AllowDeleted = options.AllowDeleted,
            GreaterThanOrEqualToID = options.GreaterThanOrEqualToID,
            LessThanOrEqualToID = options.LessThanOrEqualToID,
            SpecificIds = options.SpecificIds,
        };
    }

    public static GetAllResumeModelsOptions WithID(this GetAllResumeModelsOptions options, int? id)
    {
        options.ID = id;
        return options;
    }

    public static ResumeModelResponse MapToResponseFromModel(this ResumeModel model)
    {
        return new ResumeModelResponse
        {
            Id = model.CommonIdentity,
            Name = model.Name,
            
			HeroStatement = model.HeroStatement,
			Degrees = model.Degrees,
			Certificates = model.Certificates,
			WorkExperience = model.WorkExperience,
			Projects = model.Projects,
			PreferedName = model.PreferedName,
			Pronouns = model.Pronouns,
			Emails = model.Emails,
			Socials = model.Socials,
			Addresses = model.Addresses,
			PhoneNumbers = model.PhoneNumbers,
        };
    }
}

#pragma warning restore CS1591