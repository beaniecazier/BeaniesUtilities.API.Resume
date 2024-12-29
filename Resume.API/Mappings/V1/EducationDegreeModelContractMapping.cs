using BeaniesUtilities.Models.Resume;
using Gay.TCazier.Resume.BLL.Options.V1;
using Gay.TCazier.Resume.Contracts.Requests.V1.Create;
using Gay.TCazier.Resume.Contracts.Requests.V1.GetAll;
using Gay.TCazier.Resume.Contracts.Requests.V1.Update;
using Gay.TCazier.Resume.Contracts.Responses.V1;
using Microsoft.Data.SqlClient;

namespace Gay.TCazier.Resume.API.Mappings.V1;

#pragma warning disable CS1591

public static class EducationDegreeModelContractMapping
{
    public static EducationDegreeModel MapToModelFromCreateRequest(this CreateEducationDegreeModelRequest request,
        int id, string username, EducationInstitutionModel institution)
    {
        return new EducationDegreeModel(id, request.Name, username, request.Notes)
        {
			GPA = request.GPA,
			StartDate = request.StartDate,
			EndDate = request.EndDate,
			Institution = institution,
        };
    }

    public static EducationDegreeModel MapToModelFromUpdateRequest(this UpdateEducationDegreeModelRequest request, EducationDegreeModel model,
        string username, EducationInstitutionModel institution)
    {
        string name = request.Name is null ? model.Name : request.Name;
        return new EducationDegreeModel(request.Id, name, username, request.Notes)
        {
			GPA = request.GPA is null ? model.GPA : request.GPA.Value,
			StartDate = request.StartDate is null ? model.StartDate : request.StartDate.Value,
			EndDate = request.EndDate is null ? model.EndDate : request.EndDate.Value,
			Institution = request.Institution is null ? model.Institution : institution,
        };
    }

    public static GetAllEducationDegreeModelsOptions MapToOptions(this GetAllEducationDegreeModelsRequest request)
    {
        return new GetAllEducationDegreeModelsOptions()
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

    public static GetAllEducationDegreeModelsOptions WithID(this GetAllEducationDegreeModelsOptions options, int? id)
    {
        options.ID = id;
        return options;
    }

    public static EducationDegreeModelResponse MapToResponseFromModel(this EducationDegreeModel model)
    {
        return new EducationDegreeModelResponse
        {
            Id = model.CommonIdentity,
            Name = model.Name,
            
			GPA = model.GPA,
			StartDate = model.StartDate,
			EndDate = model.EndDate,
			Institution = model.Institution,
        };
    }
}

#pragma warning restore CS1591