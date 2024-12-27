using Gay.TCazier.Resume.Contracts.Requests.V1.Update;
using Gay.TCazier.Resume.Contracts.Responses.V1;

namespace Resume.API.Tests.Integration.Mappings.V1;

public static class ResumeModelTestMappingExtensions
{
    public static UpdateResumeModelRequest MapToUpdateRequest(this ResumeModelResponse createdModel)
    {
        return new UpdateResumeModelRequest()
        {
            Id = createdModel.Id,
            Notes = "Model updated for test purposes only",
            Name = "Updated Test WorkExperience Model",

            HeroStatement = createdModel.HeroStatement,
            Degrees = createdModel.Degrees.Select(x => x.CommonIdentity).ToArray(),
            Certificates = createdModel.Certificates.Select(x => x.CommonIdentity).ToArray(),
            WorkExperience = createdModel.WorkExperience.Select(x => x.CommonIdentity).ToArray(),
            Projects = createdModel.Projects.Select(x => x.CommonIdentity).ToArray(),
            PreferedName = createdModel.PreferedName,
            Pronouns = createdModel.Pronouns,
            Emails = createdModel.Emails,
            Socials = createdModel.Socials,
            Addresses = createdModel.Addresses.Select(x => x.CommonIdentity).ToArray(),
            PhoneNumbers = createdModel.PhoneNumbers.Select(x => x.CommonIdentity).ToArray(),
        };
    }
}
