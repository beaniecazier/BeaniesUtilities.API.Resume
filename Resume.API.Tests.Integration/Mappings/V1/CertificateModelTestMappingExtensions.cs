using Gay.TCazier.Resume.Contracts.Requests.V1;
using Gay.TCazier.Resume.Contracts.Requests.V1.Update;
using Gay.TCazier.Resume.Contracts.Responses.V1;

namespace Resume.API.Tests.Integration.Mappings.V1;

public static class CertificateModelTestMappingExtensions
{
    public static UpdateCertificateModelRequest MapToUpdateRequest(this CertificateModelResponse createdModel)
    {
        return new UpdateCertificateModelRequest()
        {
            Id = createdModel.Id,
            Notes = "Model updated for test purposes only",
            Name = "Updated Test WorkExperience Model",

            IssueDate = createdModel.IssueDate,
            Link = createdModel.Link,
            PdfFileName = createdModel.PdfFileName,
            Issuer = createdModel.Issuer.CommonIdentity,
            CertificateID = createdModel.CertificateID,
            ExpirationDate = createdModel.ExpirationDate,
        };
    }
}
