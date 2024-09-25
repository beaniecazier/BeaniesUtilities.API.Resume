using BeaniesUtilities.Models.Resume;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;
using LanguageExt;

namespace Gay.TCazier.DatabaseParser.Services.Interfaces;

public interface IWorkExperienceService
{
    public Task<Fin<IEnumerable<WorkExperienceModel>>> GetAllAsync();
    public Task<Fin<IEnumerable<WorkExperienceModel>>> GetAllWithinIDRangeAsync(int start, int end);
    public Task<Fin<IEnumerable<WorkExperienceModel>>> GetAllWithinEntryIDRangeAsync(int start, int end);
    public Task<Fin<WorkExperienceModel>> GetByIDAsync(int id);
    public Task<Fin<WorkExperienceModel>> GetByEntryIDAsync(int id);
    public Task<Fin<WorkExperienceModel>> GetHistroyOfIDAsync(int id);

    public Task<Fin<IEnumerable<WorkExperienceModel>>> SearchByNameAsync(string searchTerm);
    public Task<Fin<IEnumerable<WorkExperienceModel>>> SearchByNotesAsync(string searchTerm);
    public Task<Fin<IEnumerable<WorkExperienceModel>>> SearchBetweenModificationDatesAsync(DateTime start, DateTime end);
    public Task<Fin<IEnumerable<WorkExperienceModel>>> SearchByIsHiddenAsync(string searchTerm);
    public Task<Fin<IEnumerable<WorkExperienceModel>>> SearchByIsDeletedAsync(string searchTerm);

    public Task<Fin<IEnumerable<WorkExperienceModel>>> SearchByTechAsync(string searchTerm);
    public Task<Fin<IEnumerable<WorkExperienceModel>>> SearchByCompanyAsync(string searchTerm);
    public Task<Fin<IEnumerable<WorkExperienceModel>>> SearchBetweenDates(DateTime start, DateTime end);
    public Task<Fin<IEnumerable<WorkExperienceModel>>> SearchByResponsibility(string searchTerm);

    public Task<Fin<WorkExperienceModel>> CreateAsync(EditibleWorkExperienceModel editibleAttributes);

    public Task<Fin<WorkExperienceModel>> UpdateAsync(EditibleWorkExperienceModel editibleAttributes);

    public Task<Fin<WorkExperienceModel>> DeleteAsync();
}