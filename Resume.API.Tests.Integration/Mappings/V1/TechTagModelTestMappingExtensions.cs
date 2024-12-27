using Gay.TCazier.Resume.Contracts.Requests.V1.Update;
using Gay.TCazier.Resume.Contracts.Responses.V1;

namespace Resume.API.Tests.Integration.Mappings.V1;

public static class TechTagModelTestMappingExtensions
{
    public static UpdateTechTagModelRequest MapToUpdateRequest(this TechTagModelResponse createdModel)
    {
        return new UpdateTechTagModelRequest()
        {
            Id = createdModel.Id,
            Notes = "Model updated for test purposes only",
            Name = "Updated Test WorkExperience Model",

            URL = createdModel.URL,
            Description = createdModel.Description,
        };
    }
}
