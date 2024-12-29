using BeaniesUtilities.Models;
using BeaniesUtilities.Models.Resume;
using Gay.TCazier.Resume.BLL.Contexts;
using Gay.TCazier.Resume.BLL.Options.V1;
using LanguageExt;
using LanguageExt.Common;

namespace Gay.TCazier.Resume.BLL.Repositories.Interfaces;

public interface IPhoneNumberModelRepository
{
    Task<int> GetNextAvailableId();

    Task<int> GetQueryTotal(GetAllPhoneNumberModelsOptions options);

    Task<Fin<int>> TryCreateAsync(PhoneNumberModel model, CancellationToken token = default);
    Task<int> CreateAsync(PhoneNumberModel model, ResumeContext ctx, CancellationToken token = default);

    Task<Fin<IEnumerable<PhoneNumberModel>>> TryGetAllAsync(GetAllPhoneNumberModelsOptions options, CancellationToken token = default);
    Task<IEnumerable<PhoneNumberModel>> GetAllAsync(GetAllPhoneNumberModelsOptions options, ResumeContext ctx, CancellationToken token = default);

    Task<Fin<PhoneNumberModel>> TryGetByIdAsync(int id, CancellationToken token = default);
    Task<PhoneNumberModel> GetByIdAsync(int id, ResumeContext ctx, CancellationToken token = default);

    Task<Fin<int>> TryUpdateAsync(PhoneNumberModel model, CancellationToken token = default);
    Task<int> UpdateAsync(PhoneNumberModel model, ResumeContext ctx, CancellationToken token = default);
    Task<Fin<int>> TryUpdateToHiddenAsync(PhoneNumberModel model, CancellationToken token = default);
    Task<int> UpdateToHiddenAsync(PhoneNumberModel model, ResumeContext ctx, CancellationToken token = default);

    Task<Fin<int>> TryDeleteAsync(int id, CancellationToken token = default);
    Task<int> DeleteAsync(int id, ResumeContext ctx, CancellationToken token = default);
}