using Gay.TCazier.Resume.Contracts.Requests.V1.Update;
using Gay.TCazier.Resume.Contracts.Responses.V1;

namespace Resume.API.Tests.Integration.Mappings.V1;

public static class EducationDegreeModelTestMappingExtensions
{
    public static UpdateEducationDegreeModelRequest MapToUpdateRequest(this EducationDegreeModelResponse createdModel)
    {
        return new UpdateEducationDegreeModelRequest()
        {
            Id = createdModel.Id,
            Notes = "Model updated for test purposes only",
            Name = "Updated Test WorkExperience Model",

            GPA = createdModel.GPA,
            StartDate = createdModel.StartDate,
            EndDate = createdModel.EndDate,
            Institution = createdModel.Institution.CommonIdentity,
        };
    }
}
