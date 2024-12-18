using BeaniesUtilities.Models.Resume;
using FluentValidation.Results;
using Gay.TCazier.Resume.BLL.Options.V1;
using LanguageExt;

namespace Gay.TCazier.Resume.BLL.Services.Interfaces;

#pragma warning disable CS1591

public interface IPhoneNumberModelService
{
    Task<int> GetNextAvailableId();

    Task<IEnumerable<ValidationFailure>> ValidateModelForCreation(PhoneNumberModel model);
    Task<IEnumerable<ValidationFailure>> ValidateModelForUpdate(PhoneNumberModel model);
    Task<IEnumerable<ValidationFailure>> ValidateGetAllModelOptions(GetAllPhoneNumberModelsOptions options);

    Task<Fin<IEnumerable<PhoneNumberModel>>> GetAllAsync(GetAllPhoneNumberModelsOptions options, CancellationToken token = default);
    Task<Fin<PhoneNumberModel?>> GetByIDAsync(int id, CancellationToken token = default);

    //ADD YOUR MODEL SPECIFIC QUERY SERVICE FUNCTIONS HERE

    Task<Fin<int>> CreateAsync(PhoneNumberModel request, CancellationToken token = default);

    Task<Fin<int>> UpdateAsync(PhoneNumberModel request, PhoneNumberModel oldModel, CancellationToken token = default);

    Task<Fin<PhoneNumberModel>> DeleteAsync(int id, CancellationToken token = default);
}

#pragma warning restore CS1591