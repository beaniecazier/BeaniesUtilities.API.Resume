using BeaniesUtilities.Models.Resume;
using Gay.TCazier.Resume.BLL.Options.V1;
using Gay.TCazier.Resume.Contracts.Requests.V1.Create;
using Gay.TCazier.Resume.Contracts.Requests.V1.GetAll;
using Gay.TCazier.Resume.Contracts.Requests.V1.Update;
using Gay.TCazier.Resume.Contracts.Responses.V1;
using Microsoft.Data.SqlClient;

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
			CertificateID = request.CertificateID,
			ExpirationDate = request.ExpirationDate,
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
			CertificateID = string.IsNullOrWhiteSpace(request.CertificateID) ? model.CertificateID : request.CertificateID,
			ExpirationDate = request.ExpirationDate is null ? model.ExpirationDate : request.ExpirationDate.Value,
        };
    }

    public static GetAllCertificateModelsOptions MapToOptions(this GetAllCertificateModelsRequest request)
    {
        return new GetAllCertificateModelsOptions()
        {
            NameSearchTerm = request.NameSearchTerm,
            NotesSearchTerm = request.NotesSearchTerm,
            AfterDate = request.AfterDate,
            BeforeDate = request.BeforeDate,
            AllowHidden = request.AllowHidden,
            AllowDeleted = request.AllowDeleted,
            GreaterThanOrEqualToID = request.GreaterThanOrEqualToID,
            LessThanOrEqualToID = request.LessThanOrEqualToID,
            SpecificIds = request.SpecificIds,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            SortField = request.SortBy?.Trim('+', '-'),
            SortOrder = request.SortBy is null ? SortOrder.Unspecified : 
                request.SortBy.StartsWith('-') ? SortOrder.Descending : SortOrder.Ascending,
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
			CertificateID = model.CertificateID,
			ExpirationDate = model.ExpirationDate,
        };
    }
}

#pragma warning restore CS1591