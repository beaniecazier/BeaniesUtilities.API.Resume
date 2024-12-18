using BeaniesUtilities.Models.Resume;
using FluentValidation.Results;
using Gay.TCazier.Resume.BLL.Options.V1;
using LanguageExt;

namespace Gay.TCazier.Resume.BLL.Services.Interfaces;

#pragma warning disable CS1591

public interface IWorkExperienceModelService
{
    Task<int> GetNextAvailableId();

    Task<IEnumerable<ValidationFailure>> ValidateModelForCreation(WorkExperienceModel model);
    Task<IEnumerable<ValidationFailure>> ValidateModelForUpdate(WorkExperienceModel model);
    Task<IEnumerable<ValidationFailure>> ValidateGetAllModelOptions(GetAllWorkExperienceModelsOptions options);

    Task<Fin<IEnumerable<WorkExperienceModel>>> GetAllAsync(GetAllWorkExperienceModelsOptions options, CancellationToken token = default);
    Task<Fin<WorkExperienceModel?>> GetByIDAsync(int id, CancellationToken token = default);

    //ADD YOUR MODEL SPECIFIC QUERY SERVICE FUNCTIONS HERE

    Task<Fin<int>> CreateAsync(WorkExperienceModel request, CancellationToken token = default);

    Task<Fin<int>> UpdateAsync(WorkExperienceModel request, WorkExperienceModel oldModel, CancellationToken token = default);

    Task<Fin<WorkExperienceModel>> DeleteAsync(int id, CancellationToken token = default);
}

#pragma warning restore CS1591