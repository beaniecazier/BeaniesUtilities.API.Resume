using BeaniesUtilities.Models.Resume;
using FluentValidation.Results;
using Gay.TCazier.Resume.BLL.Options.V1;
using LanguageExt;

namespace Gay.TCazier.Resume.BLL.Services.Interfaces;

#pragma warning disable CS1591

public interface IResumeModelService
{
    Task<int> GetNextAvailableId();

    Task<IEnumerable<ValidationFailure>> ValidateModelForCreation(ResumeModel model);
    Task<IEnumerable<ValidationFailure>> ValidateModelForUpdate(ResumeModel model);
    Task<IEnumerable<ValidationFailure>> ValidateGetAllModelOptions(GetAllResumeModelsOptions options);

    Task<Fin<IEnumerable<ResumeModel>>> GetAllAsync(GetAllResumeModelsOptions options, CancellationToken token = default);
    Task<Fin<ResumeModel?>> GetByIDAsync(int id, CancellationToken token = default);

    //ADD YOUR MODEL SPECIFIC QUERY SERVICE FUNCTIONS HERE

    Task<Fin<int>> CreateAsync(ResumeModel request, CancellationToken token = default);

    Task<Fin<int>> UpdateAsync(ResumeModel request, ResumeModel oldModel, CancellationToken token = default);

    Task<Fin<ResumeModel>> DeleteAsync(int id, CancellationToken token = default);
}

#pragma warning restore CS1591