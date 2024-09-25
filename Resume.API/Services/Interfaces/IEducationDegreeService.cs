using BeaniesUtilities.Models.Resume;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;
using LanguageExt;

namespace Gay.TCazier.DatabaseParser.Services.Interfaces;

public interface IEducationDegreeService
{
    public Task<Fin<IEnumerable<EducationDegreeModel>>> GetAllAsync();
    public Task<Fin<IEnumerable<EducationDegreeModel>>> GetAllWithinIDRangeAsync(int start, int end);
    public Task<Fin<IEnumerable<EducationDegreeModel>>> GetAllWithinEntryIDRangeAsync(int start, int end);
    public Task<Fin<EducationDegreeModel>> GetByIDAsync(int id);
    public Task<Fin<EducationDegreeModel>> GetByEntryIDAsync(int id);
    public Task<Fin<EducationDegreeModel>> GetHistroyOfIDAsync(int id);

    public Task<Fin<IEnumerable<EducationDegreeModel>>> SearchByNameAsync(string searchTerm);
    public Task<Fin<IEnumerable<EducationDegreeModel>>> SearchByNotesAsync(string searchTerm);
    public Task<Fin<IEnumerable<EducationDegreeModel>>> SearchBetweenModificationDatesAsync(DateTime start, DateTime end);
    public Task<Fin<IEnumerable<EducationDegreeModel>>> SearchByIsHiddenAsync(string searchTerm);
    public Task<Fin<IEnumerable<EducationDegreeModel>>> SearchByIsDeletedAsync(string searchTerm);

    public Task<Fin<IEnumerable<EducationDegreeModel>>> SearchBetweenDates(DateTime start, DateTime end);
    public Task<Fin<IEnumerable<EducationDegreeModel>>> SearchByInstitute(string searchTerm);

    public Task<Fin<EducationDegreeModel>> CreateAsync(EditibleEduDegreeModel editibleAttributes);

    public Task<Fin<EducationDegreeModel>> UpdateAsync(EditibleEduDegreeModel editibleAttributes);

    public Task<Fin<EducationDegreeModel>> DeleteAsync();
}