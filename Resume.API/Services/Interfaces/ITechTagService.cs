using BeaniesUtilities.Models.Resume;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;
using LanguageExt;

namespace Gay.TCazier.DatabaseParser.Services.Interfaces;

public interface ITechTagService
{
    public Task<Fin<IEnumerable<TechTagModel>>> GetAllAsync();
    public Task<Fin<IEnumerable<TechTagModel>>> GetAllWithinIDRangeAsync(int start, int end);
    public Task<Fin<IEnumerable<TechTagModel>>> GetAllWithinEntryIDRangeAsync(int start, int end);
    public Task<Fin<TechTagModel>> GetByIDAsync(int id);
    public Task<Fin<TechTagModel>> GetByEntryIDAsync(int id);
    public Task<Fin<TechTagModel>> GetHistroyOfIDAsync(int id);

    public Task<Fin<IEnumerable<TechTagModel>>> SearchByNameAsync(string searchTerm);
    public Task<Fin<IEnumerable<TechTagModel>>> SearchByNotesAsync(string searchTerm);
    public Task<Fin<IEnumerable<TechTagModel>>> SearchBetweenModificationDatesAsync(DateTime start, DateTime end);
    public Task<Fin<IEnumerable<TechTagModel>>> SearchByIsHiddenAsync(string searchTerm);
    public Task<Fin<IEnumerable<TechTagModel>>> SearchByIsDeletedAsync(string searchTerm);

    public Task<Fin<IEnumerable<TechTagModel>>> SearchByDescriptionAsync();

    public Task<Fin<TechTagModel>> CreateAsync(EditibleTechTagModel editibleAttributes);

    public Task<Fin<TechTagModel>> UpdateAsync(EditibleTechTagModel editibleAttributes);

    public Task<Fin<TechTagModel>> DeleteAsync();
}