using BeaniesUtilities.Models.Resume;
using FluentValidation.Results;
using Gay.TCazier.Resume.BLL.Options.V1;
using LanguageExt;

namespace Gay.TCazier.Resume.BLL.Services.Interfaces;

#pragma warning disable CS1591

public interface IPersonModelService
{
    Task<int> GetNextAvailableId();

    Task<int> GetQueryTotal(GetAllPersonModelsOptions options);

    Task<IEnumerable<ValidationFailure>> ValidateModelForCreation(PersonModel model);
    Task<IEnumerable<ValidationFailure>> ValidateModelForUpdate(PersonModel model);
    Task<IEnumerable<ValidationFailure>> ValidateGetAllModelOptions(GetAllPersonModelsOptions options);

    Task<Fin<IEnumerable<PersonModel>>> GetAllAsync(GetAllPersonModelsOptions options, CancellationToken token = default);
    Task<Fin<PersonModel?>> GetByIDAsync(int id, CancellationToken token = default);

    //ADD YOUR MODEL SPECIFIC QUERY SERVICE FUNCTIONS HERE

    Task<Fin<int>> CreateAsync(PersonModel request, CancellationToken token = default);

    Task<Fin<int>> UpdateAsync(PersonModel request, PersonModel oldModel, CancellationToken token = default);

    Task<Fin<PersonModel>> DeleteAsync(int id, CancellationToken token = default);
}

#pragma warning restore CS1591