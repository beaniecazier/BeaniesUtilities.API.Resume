using BeaniesUtilities.Models.Resume;
using FluentValidation.Results;
using LanguageExt;

namespace Gay.TCazier.Resume.BLL.Services.Interfaces;

#pragma warning disable CS1591

public interface IAddressService
{
    Task<int> GetNextAvailableId();

    Task<IEnumerable<ValidationFailure>> ValidateModelForCreation(AddressModel model);
    Task<IEnumerable<ValidationFailure>> ValidateModelForUpdate(AddressModel model);

    Task<Fin<IEnumerable<AddressModel>>> GetAllAsync(CancellationToken token = default);
    Task<Fin<AddressModel?>> GetByIDAsync(int id, CancellationToken token = default);

    //ADD YOUR MODEL SPECIFIC QUERY SERVICE FUNCTIONS HERE

    Task<Fin<int>> CreateAsync(AddressModel request, CancellationToken token = default);

    Task<Fin<int>> UpdateAsync(AddressModel request, AddressModel oldModel, CancellationToken token = default);

    Task<Fin<AddressModel>> DeleteAsync(int id, CancellationToken token = default);
}

#pragma warning restore CS1591