using BeaniesUtilities.Models.Resume;
using Gay.TCazier.Resume.BLL.Options.V1;
using Gay.TCazier.Resume.Contracts.Requests.V1.Create;
using Gay.TCazier.Resume.Contracts.Requests.V1.GetAll;
using Gay.TCazier.Resume.Contracts.Requests.V1.Update;
using Gay.TCazier.Resume.Contracts.Responses.V1;
using Microsoft.Data.SqlClient;

namespace Gay.TCazier.Resume.API.Mappings.V1;

#pragma warning disable CS1591

public static class EducationInstitutionModelContractMapping
{
    public static EducationInstitutionModel MapToModelFromCreateRequest(this CreateEducationInstitutionModelRequest request,
        int id, string username, AddressModel address)
    {
        return new EducationInstitutionModel(id, request.Name, username, request.Notes)
        {
			Website = request.Website,
			Address = address,
        };
    }

    public static EducationInstitutionModel MapToModelFromUpdateRequest(this UpdateEducationInstitutionModelRequest request, EducationInstitutionModel model,
        string username, AddressModel address)
    {
        string name = request.Name is null ? model.Name : request.Name;
        return new EducationInstitutionModel(request.Id, name, username, request.Notes)
        {
			Website = string.IsNullOrWhiteSpace(request.Website) ? model.Website : request.Website,
			Address = request.Address is null ? model.Address : address,
        };
    }

    public static GetAllEducationInstitutionModelsOptions MapToOptions(this GetAllEducationInstitutionModelsRequest request)
    {
        return new GetAllEducationInstitutionModelsOptions()
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

    public static GetAllEducationInstitutionModelsOptions WithID(this GetAllEducationInstitutionModelsOptions options, int? id)
    {
        options.ID = id;
        return options;
    }

    public static EducationInstitutionModelResponse MapToResponseFromModel(this EducationInstitutionModel model)
    {
        return new EducationInstitutionModelResponse
        {
            Id = model.CommonIdentity,
            Name = model.Name,
            
			Website = model.Website,
			Address = model.Address,
        };
    }
}

#pragma warning restore CS1591