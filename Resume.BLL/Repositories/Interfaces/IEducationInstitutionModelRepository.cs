using BeaniesUtilities.Models;
using BeaniesUtilities.Models.Resume;
using Gay.TCazier.Resume.BLL.Contexts;
using Gay.TCazier.Resume.BLL.Options.V1;
using LanguageExt;
using LanguageExt.Common;

namespace Gay.TCazier.Resume.BLL.Repositories.Interfaces;

public interface IEducationInstitutionModelRepository
{
    Task<int> GetNextAvailableId();

    Task<Fin<int>> TryCreateAsync(EducationInstitutionModel model, CancellationToken token = default);
    Task<int> CreateAsync(EducationInstitutionModel model, ResumeContext ctx, CancellationToken token = default);

    Task<Fin<IEnumerable<EducationInstitutionModel>>> TryGetAllAsync(GetAllEducationInstitutionModelsOptions options, CancellationToken token = default);
    Task<IEnumerable<EducationInstitutionModel>> GetAllAsync(GetAllEducationInstitutionModelsOptions options, ResumeContext ctx, CancellationToken token = default);

    Task<Fin<EducationInstitutionModel>> TryGetByIdAsync(int id, CancellationToken token = default);
    Task<EducationInstitutionModel> GetByIdAsync(int id, ResumeContext ctx, CancellationToken token = default);

    Task<Fin<int>> TryUpdateAsync(EducationInstitutionModel model, CancellationToken token = default);
    Task<int> UpdateAsync(EducationInstitutionModel model, ResumeContext ctx, CancellationToken token = default);
    Task<Fin<int>> TryUpdateToHiddenAsync(EducationInstitutionModel model, CancellationToken token = default);
    Task<int> UpdateToHiddenAsync(EducationInstitutionModel model, ResumeContext ctx, CancellationToken token = default);

    Task<Fin<int>> TryDeleteAsync(int id, CancellationToken token = default);
    Task<int> DeleteAsync(int id, ResumeContext ctx, CancellationToken token = default);
}