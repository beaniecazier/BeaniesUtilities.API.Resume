using Gay.TCazier.Resume.Contracts.Requests.V1.Update;
using Gay.TCazier.Resume.Contracts.Responses.V1;

namespace Resume.API.Tests.Integration.Mappings.V1;

public static class EducationInstitutionModelTestMappingExtensions
{
    public static UpdateEducationInstitutionModelRequest MapToUpdateRequest(this EducationInstitutionModelResponse createdModel)
    {
        return new UpdateEducationInstitutionModelRequest()
        {
            Id = createdModel.Id,
            Notes = "Model updated for test purposes only",
            Name = "Updated Test WorkExperience Model",

            Website = createdModel.Website,
            Address = createdModel.Address.CommonIdentity,
            //CertificatesIssued = model.CertificatesIssued,
            //DegreesGiven = model.DegreesGiven,
        };
    }
}
