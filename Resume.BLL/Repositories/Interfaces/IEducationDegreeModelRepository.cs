using BeaniesUtilities.Models;
using BeaniesUtilities.Models.Resume;
using Gay.TCazier.Resume.BLL.Contexts;
using Gay.TCazier.Resume.BLL.Options.V1;
using LanguageExt;
using LanguageExt.Common;

namespace Gay.TCazier.Resume.BLL.Repositories.Interfaces;

public interface IEducationDegreeModelRepository
{
    Task<int> GetNextAvailableId();

    Task<int> GetQueryTotal(GetAllEducationDegreeModelsOptions options);

    Task<Fin<int>> TryCreateAsync(EducationDegreeModel model, CancellationToken token = default);
    Task<int> CreateAsync(EducationDegreeModel model, ResumeContext ctx, CancellationToken token = default);

    Task<Fin<IEnumerable<EducationDegreeModel>>> TryGetAllAsync(GetAllEducationDegreeModelsOptions options, CancellationToken token = default);
    Task<IEnumerable<EducationDegreeModel>> GetAllAsync(GetAllEducationDegreeModelsOptions options, ResumeContext ctx, CancellationToken token = default);

    Task<Fin<EducationDegreeModel>> TryGetByIdAsync(int id, CancellationToken token = default);
    Task<EducationDegreeModel> GetByIdAsync(int id, ResumeContext ctx, CancellationToken token = default);

    Task<Fin<int>> TryUpdateAsync(EducationDegreeModel model, CancellationToken token = default);
    Task<int> UpdateAsync(EducationDegreeModel model, ResumeContext ctx, CancellationToken token = default);
    Task<Fin<int>> TryUpdateToHiddenAsync(EducationDegreeModel model, CancellationToken token = default);
    Task<int> UpdateToHiddenAsync(EducationDegreeModel model, ResumeContext ctx, CancellationToken token = default);

    Task<Fin<int>> TryDeleteAsync(int id, CancellationToken token = default);
    Task<int> DeleteAsync(int id, ResumeContext ctx, CancellationToken token = default);
}