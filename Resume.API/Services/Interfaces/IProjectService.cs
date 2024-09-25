using BeaniesUtilities.Models.Resume;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;
using LanguageExt;

namespace Gay.TCazier.DatabaseParser.Services.Interfaces;

public interface IProjectService
{
    public Task<Fin<IEnumerable<ProjectModel>>> GetAllAsync();
    public Task<Fin<IEnumerable<ProjectModel>>> GetAllWithinIDRangeAsync(int start, int end);
    public Task<Fin<IEnumerable<ProjectModel>>> GetAllWithinEntryIDRangeAsync(int start, int end);
    public Task<Fin<ProjectModel>> GetByIDAsync(int id);
    public Task<Fin<ProjectModel>> GetByEntryIDAsync(int id);
    public Task<Fin<ProjectModel>> GetHistroyOfIDAsync(int id);

    public Task<Fin<IEnumerable<ProjectModel>>> SearchByNameAsync(string searchTerm);
    public Task<Fin<IEnumerable<ProjectModel>>> SearchByNotesAsync(string searchTerm);
    public Task<Fin<IEnumerable<ProjectModel>>> SearchBetweenModificationDatesAsync(DateTime start, DateTime end);
    public Task<Fin<IEnumerable<ProjectModel>>> SearchByIsHiddenAsync(string searchTerm);
    public Task<Fin<IEnumerable<ProjectModel>>> SearchByIsDeletedAsync(string searchTerm);

    public Task<Fin<IEnumerable<ProjectModel>>> SearchByTechAsync(string searchTerm);
    public Task<Fin<IEnumerable<ProjectModel>>> SearchByDescriptionAsync(string searchTerm);
    public Task<Fin<IEnumerable<ProjectModel>>> SearchBetweenDates(DateTime start, DateTime end);

    public Task<Fin<ProjectModel>> CreateAsync(EditibleProjectModel editibleAttributes);

    public Task<Fin<ProjectModel>> UpdateAsync(EditibleProjectModel editibleAttributes);

    public Task<Fin<ProjectModel>> DeleteAsync();
}
