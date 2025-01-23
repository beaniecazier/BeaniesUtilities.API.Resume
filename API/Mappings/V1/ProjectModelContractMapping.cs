using BeaniesUtilities.Models.Resume;
using Gay.TCazier.Resume.BLL.Options.V1;
using Gay.TCazier.Resume.Contracts.Requests.V1.Create;
using Gay.TCazier.Resume.Contracts.Requests.V1.GetAll;
using Gay.TCazier.Resume.Contracts.Requests.V1.Update;
using Gay.TCazier.Resume.Contracts.Responses.V1;
using Microsoft.Data.SqlClient;

namespace Gay.TCazier.Resume.API.Mappings.V1;

#pragma warning disable CS1591

public static class ProjectModelContractMapping
{
    public static ProjectModel MapToModelFromCreateRequest(this CreateProjectModelRequest request,
        int id, string username, List<TechTagModel> techTagses)
    {
        return new ProjectModel(id, request.Name, username, request.Notes)
        {
			Description = request.Description,
			Version = request.Version,
			ProjectUrl = request.ProjectUrl,
			StartDate = request.StartDate,
			CompletionDate = request.CompletionDate,
			TechTags = techTagses,
        };
    }

    public static ProjectModel MapToModelFromUpdateRequest(this UpdateProjectModelRequest request, string username, List<TechTagModel> techTagses)
    {
        string name = request.Name;
        return new ProjectModel(request.Id, name, username, request.Notes)
        {
			Description = request.Description,
			Version = request.Version,
			ProjectUrl = request.ProjectUrl,
			StartDate = request.StartDate.Value,
			CompletionDate = request.CompletionDate.Value,
			TechTags = techTagses,
        };
    }

    public static GetAllProjectModelsOptions MapToOptions(this GetAllProjectModelsRequest request)
    {
        return new GetAllProjectModelsOptions()
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

    public static GetAllProjectModelsOptions WithID(this GetAllProjectModelsOptions options, int? id)
    {
        options.ID = id;
        return options;
    }

    public static ProjectModelResponse MapToResponseFromModel(this ProjectModel model)
    {
        return new ProjectModelResponse
        {
            Id = model.CommonIdentity,
            Name = model.Name,
            
			Description = model.Description,
			Version = model.Version,
			ProjectUrl = model.ProjectUrl,
			StartDate = model.StartDate,
			CompletionDate = model.CompletionDate,
			TechTags = model.TechTags,
        };
    }
}

#pragma warning restore CS1591