using BeaniesUtilities.Models.Resume;
using Gay.TCazier.Resume.BLL.Options.V1;
using Gay.TCazier.Resume.Contracts.Requests.V1;
using Gay.TCazier.Resume.Contracts.Responses.V1;

namespace Gay.TCazier.Resume.API.Mappings.V1;

#pragma warning disable CS1591

public static class CertificateModelContractMapping
{
    public static CertificateModel MapToModelFromCreateRequest(this CreateCertificateModelRequest request,
        int id, string username, EducationInstitutionModel issuer)
    {
        return new CertificateModel(id, request.Name, username, request.Notes)
        {
			IssueDate = request.IssueDate,
			Link = request.Link,
			PdfFileName = request.PdfFileName,
			Issuer = issuer,
        };
    }

    public static CertificateModel MapToModelFromUpdateRequest(this UpdateCertificateModelRequest request, CertificateModel model,
        string username, EducationInstitutionModel issuer)
    {
        string name = request.Name is null ? model.Name : request.Name;
        return new CertificateModel(request.Id, name, username, request.Notes)
        {
			IssueDate = request.IssueDate is null ? model.IssueDate : request.IssueDate.Value,
			Link = string.IsNullOrWhiteSpace(request.Link) ? model.Link : request.Link,
			PdfFileName = string.IsNullOrWhiteSpace(request.PdfFileName) ? model.PdfFileName : request.PdfFileName,
			Issuer = request.Issuer is null ? model.Issuer : issuer,
        };
    }

    public static GetAllCertificateModelsOptions MapToOptions(this GetAllCertificateModelsRequest options)
    {
        return new GetAllCertificateModelsOptions()
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

    public static GetAllCertificateModelsOptions WithID(this GetAllCertificateModelsOptions options, int? id)
    {
        options.ID = id;
        return options;
    }

    public static CertificateModelResponse MapToResponseFromModel(this CertificateModel model)
    {
        return new CertificateModelResponse
        {
            Id = model.CommonIdentity,
            Name = model.Name,
            
			IssueDate = model.IssueDate,
			Link = model.Link,
			PdfFileName = model.PdfFileName,
			Issuer = model.Issuer,
        };
    }
}

#pragma warning restore CS1591