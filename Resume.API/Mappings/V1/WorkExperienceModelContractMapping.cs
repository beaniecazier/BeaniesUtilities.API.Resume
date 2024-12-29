using BeaniesUtilities.Models.Resume;
using Gay.TCazier.Resume.BLL.Options.V1;
using Gay.TCazier.Resume.Contracts.Requests.V1.Create;
using Gay.TCazier.Resume.Contracts.Requests.V1.GetAll;
using Gay.TCazier.Resume.Contracts.Requests.V1.Update;
using Gay.TCazier.Resume.Contracts.Responses.V1;
using Microsoft.Data.SqlClient;

namespace Gay.TCazier.Resume.API.Mappings.V1;

#pragma warning disable CS1591

public static class WorkExperienceModelContractMapping
{
    public static WorkExperienceModel MapToModelFromCreateRequest(this CreateWorkExperienceModelRequest request,
        int id, string username, List<TechTagModel> techUsedes)
    {
        return new WorkExperienceModel(id, request.Name, username, request.Notes)
        {
			StartDate = request.StartDate,
			EndDate = request.EndDate,
			Company = request.Company,
			Description = request.Description,
			Responsibilities = request.Responsibilities,
			TechUsed = techUsedes,
        };
    }

    public static WorkExperienceModel MapToModelFromUpdateRequest(this UpdateWorkExperienceModelRequest request, WorkExperienceModel model,
        string username, List<TechTagModel> techUsedes)
    {
        string name = request.Name is null ? model.Name : request.Name;
        return new WorkExperienceModel(request.Id, name, username, request.Notes)
        {
			StartDate = request.StartDate is null ? model.StartDate : request.StartDate.Value,
			EndDate = request.EndDate is null ? model.EndDate : request.EndDate.Value,
			Company = string.IsNullOrWhiteSpace(request.Company) ? model.Company : request.Company,
			Description = string.IsNullOrWhiteSpace(request.Description) ? model.Description : request.Description,
			Responsibilities = request.Responsibilities.Count() <= 0 ? model.Responsibilities : request.Responsibilities,
			TechUsed = request.TechUsed.Count() <= 0 ? model.TechUsed : techUsedes,
        };
    }

    public static GetAllWorkExperienceModelsOptions MapToOptions(this GetAllWorkExperienceModelsRequest request)
    {
        return new GetAllWorkExperienceModelsOptions()
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

    public static GetAllWorkExperienceModelsOptions WithID(this GetAllWorkExperienceModelsOptions options, int? id)
    {
        options.ID = id;
        return options;
    }

    public static WorkExperienceModelResponse MapToResponseFromModel(this WorkExperienceModel model)
    {
        return new WorkExperienceModelResponse
        {
            Id = model.CommonIdentity,
            Name = model.Name,
            
			StartDate = model.StartDate,
			EndDate = model.EndDate,
			Company = model.Company,
			Description = model.Description,
			Responsibilities = model.Responsibilities,
			TechUsed = model.TechUsed,
        };
    }
}

#pragma warning restore CS1591