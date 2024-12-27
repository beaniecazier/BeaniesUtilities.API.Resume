using Gay.TCazier.Resume.Contracts.Requests.V1.Update;
using Gay.TCazier.Resume.Contracts.Responses.V1;

namespace Resume.API.Tests.Integration.Mappings.V1;

public static class ProjectModelTestMappingExtensions
{
    public static UpdateProjectModelRequest MapToUpdateRequest(this ProjectModelResponse createdModel)
    {
        return new UpdateProjectModelRequest()
        {
            Id = createdModel.Id,
            Notes = "Model updated for test purposes only",
            Name = "Updated Test WorkExperience Model",

            Description = createdModel.Description,
            Version = createdModel.Version,
            ProjectUrl = createdModel.ProjectUrl,
            StartDate = createdModel.StartDate,
            CompletionDate = createdModel.CompletionDate,
            TechTags = createdModel.TechTags.Select(x => x.CommonIdentity).ToArray(),
        };
    }
}
