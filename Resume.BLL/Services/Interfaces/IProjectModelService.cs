using BeaniesUtilities.Models.Resume;
using FluentValidation.Results;
using Gay.TCazier.Resume.BLL.Options.V1;
using LanguageExt;

namespace Gay.TCazier.Resume.BLL.Services.Interfaces;

#pragma warning disable CS1591

public interface IProjectModelService
{
    Task<int> GetNextAvailableId();

    Task<int> GetQueryTotal(GetAllProjectModelsOptions options);

    Task<IEnumerable<ValidationFailure>> ValidateModelForCreation(ProjectModel model);
    Task<IEnumerable<ValidationFailure>> ValidateModelForUpdate(ProjectModel model);
    Task<IEnumerable<ValidationFailure>> ValidateGetAllModelOptions(GetAllProjectModelsOptions options);

    Task<Fin<IEnumerable<ProjectModel>>> GetAllAsync(GetAllProjectModelsOptions options, CancellationToken token = default);
    Task<Fin<ProjectModel?>> GetByIDAsync(int id, CancellationToken token = default);

    //ADD YOUR MODEL SPECIFIC QUERY SERVICE FUNCTIONS HERE

    Task<Fin<int>> CreateAsync(ProjectModel request, CancellationToken token = default);

    Task<Fin<int>> UpdateAsync(ProjectModel request, ProjectModel oldModel, CancellationToken token = default);

    Task<Fin<ProjectModel>> DeleteAsync(int id, CancellationToken token = default);
}

#pragma warning restore CS1591