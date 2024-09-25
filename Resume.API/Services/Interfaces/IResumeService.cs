using BeaniesUtilities.Models.Resume;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;
using LanguageExt;

namespace Gay.TCazier.DatabaseParser.Services.Interfaces;

public interface IResumeService
{
    public Task<Fin<IEnumerable<ResumeModel>>> GetAllAsync();
    public Task<Fin<IEnumerable<ResumeModel>>> GetAllWithinIDRangeAsync(int start, int end);
    public Task<Fin<IEnumerable<ResumeModel>>> GetAllWithinEntryIDRangeAsync(int start, int end);
    public Task<Fin<ResumeModel>> GetByIDAsync(int id);
    public Task<Fin<ResumeModel>> GetByEntryIDAsync(int id);
    public Task<Fin<ResumeModel>> GetHistroyOfIDAsync(int id);

    public Task<Fin<IEnumerable<ResumeModel>>> SearchByNameAsync(string searchTerm);
    public Task<Fin<IEnumerable<ResumeModel>>> SearchByNotesAsync(string searchTerm);
    public Task<Fin<IEnumerable<ResumeModel>>> SearchBetweenModificationDatesAsync(DateTime start, DateTime end);
    public Task<Fin<IEnumerable<ResumeModel>>> SearchByIsHiddenAsync(string searchTerm);
    public Task<Fin<IEnumerable<ResumeModel>>> SearchByIsDeletedAsync(string searchTerm);

    public Task<Fin<IEnumerable<ResumeModel>>> SearchBySocialsAsync(string searchTerm);
    public Task<Fin<IEnumerable<ResumeModel>>> SearchByEmailAsync(string searchTerm);
    public Task<Fin<IEnumerable<ResumeModel>>> SearchByPhoneNumberAsync(string searchTerm);
    public Task<Fin<IEnumerable<ResumeModel>>> SearchByPreferedNameAsync(string searchTerm);

    public Task<Fin<ResumeModel>> CreateAsync(EditibleResumeModel editibleAttributes);

    public Task<Fin<ResumeModel>> UpdateAsync(EditibleResumeModel editibleAttributes);

    public Task<Fin<ResumeModel>> DeleteAsync();
}