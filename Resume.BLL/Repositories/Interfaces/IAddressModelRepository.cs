using BeaniesUtilities.Models;
using BeaniesUtilities.Models.Resume;
using Gay.TCazier.Resume.BLL.Contexts;
using Gay.TCazier.Resume.BLL.Options.V1;
using LanguageExt;
using LanguageExt.Common;

namespace Gay.TCazier.Resume.BLL.Repositories.Interfaces;

public interface IAddressModelRepository
{
    Task<int> GetNextAvailableId();

    Task<int> GetQueryTotal(GetAllAddressModelsOptions options);

    Task<Fin<int>> TryCreateAsync(AddressModel model, CancellationToken token = default);
    Task<int> CreateAsync(AddressModel model, ResumeContext ctx, CancellationToken token = default);

    Task<Fin<IEnumerable<AddressModel>>> TryGetAllAsync(GetAllAddressModelsOptions options, CancellationToken token = default);
    Task<IEnumerable<AddressModel>> GetAllAsync(GetAllAddressModelsOptions options, ResumeContext ctx, CancellationToken token = default);

    Task<Fin<AddressModel>> TryGetByIdAsync(int id, CancellationToken token = default);
    Task<AddressModel> GetByIdAsync(int id, ResumeContext ctx, CancellationToken token = default);

    Task<Fin<int>> TryUpdateAsync(AddressModel model, CancellationToken token = default);
    Task<int> UpdateAsync(AddressModel model, ResumeContext ctx, CancellationToken token = default);
    Task<Fin<int>> TryUpdateToHiddenAsync(AddressModel model, CancellationToken token = default);
    Task<int> UpdateToHiddenAsync(AddressModel model, ResumeContext ctx, CancellationToken token = default);

    Task<Fin<int>> TryDeleteAsync(int id, CancellationToken token = default);
    Task<int> DeleteAsync(int id, ResumeContext ctx, CancellationToken token = default);
}