using Gay.TCazier.Resume.Contracts.Requests.V1.Update;
using Gay.TCazier.Resume.Contracts.Responses.V1;

namespace Resume.API.Tests.Integration.Mappings.V1;

public static class WorkExperienceModelTestMappingExtensions
{
    public static UpdateWorkExperienceModelRequest MapToUpdateRequest(this WorkExperienceModelResponse createdModel)
    {
        return new UpdateWorkExperienceModelRequest()
        {
            Id = createdModel.Id,
            Notes = "Model updated for test purposes only",
            Name = "Updated Test WorkExperience Model",

            StartDate = createdModel.StartDate,
            EndDate = createdModel.EndDate,
            Company = createdModel.Company,
            Description = createdModel.Description,
            Responsibilities = createdModel.Responsibilities,
            TechUsed = createdModel.TechUsed.Select(x => x.CommonIdentity).ToArray(),
        };
    }
}
