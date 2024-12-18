using BeaniesUtilities.Models;
using BeaniesUtilities.Models.Resume;
using Gay.TCazier.Resume.BLL.Contexts;
using Gay.TCazier.Resume.BLL.Options.V1;
using LanguageExt;
using LanguageExt.Common;

namespace Gay.TCazier.Resume.BLL.Repositories.Interfaces;

public interface IPersonModelRepository
{
    Task<int> GetNextAvailableId();

    Task<Fin<int>> TryCreateAsync(PersonModel model, CancellationToken token = default);
    Task<int> CreateAsync(PersonModel model, ResumeContext ctx, CancellationToken token = default);

    Task<Fin<IEnumerable<PersonModel>>> TryGetAllAsync(GetAllPersonModelsOptions options, CancellationToken token = default);
    Task<IEnumerable<PersonModel>> GetAllAsync(GetAllPersonModelsOptions options, ResumeContext ctx, CancellationToken token = default);

    Task<Fin<PersonModel>> TryGetByIdAsync(int id, CancellationToken token = default);
    Task<PersonModel> GetByIdAsync(int id, ResumeContext ctx, CancellationToken token = default);

    Task<Fin<int>> TryUpdateAsync(PersonModel model, CancellationToken token = default);
    Task<int> UpdateAsync(PersonModel model, ResumeContext ctx, CancellationToken token = default);
    Task<Fin<int>> TryUpdateToHiddenAsync(PersonModel model, CancellationToken token = default);
    Task<int> UpdateToHiddenAsync(PersonModel model, ResumeContext ctx, CancellationToken token = default);

    Task<Fin<int>> TryDeleteAsync(int id, CancellationToken token = default);
    Task<int> DeleteAsync(int id, ResumeContext ctx, CancellationToken token = default);
}