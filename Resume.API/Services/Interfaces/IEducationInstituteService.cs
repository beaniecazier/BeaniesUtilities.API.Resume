using BeaniesUtilities.Models.Resume;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;
using LanguageExt;

namespace Gay.TCazier.DatabaseParser.Services.Interfaces;

public interface IEducationInstituteService
{
    public Task<Fin<IEnumerable<EducationInstitutionModel>>> GetAllAsync();
    public Task<Fin<IEnumerable<EducationInstitutionModel>>> GetAllWithinIDRangeAsync(int start, int end);
    public Task<Fin<IEnumerable<EducationInstitutionModel>>> GetAllWithinEntryIDRangeAsync(int start, int end);
    public Task<Fin<EducationInstitutionModel>> GetByIDAsync(int id);
    public Task<Fin<EducationInstitutionModel>> GetByEntryIDAsync(int id);
    public Task<Fin<EducationInstitutionModel>> GetHistroyOfIDAsync(int id);

    public Task<Fin<IEnumerable<EducationInstitutionModel>>> SearchByNameAsync(string searchTerm);
    public Task<Fin<IEnumerable<EducationInstitutionModel>>> SearchByNotesAsync(string searchTerm);
    public Task<Fin<IEnumerable<EducationInstitutionModel>>> SearchBetweenModificationDatesAsync(DateTime start, DateTime end);
    public Task<Fin<IEnumerable<EducationInstitutionModel>>> SearchByIsHiddenAsync(string searchTerm);
    public Task<Fin<IEnumerable<EducationInstitutionModel>>> SearchByIsDeletedAsync(string searchTerm);

    public Task<Fin<EducationInstitutionModel>> CreateAsync(EditibleEduInstituteModel editibleAttributes);

    public Task<Fin<EducationInstitutionModel>> UpdateAsync(EditibleEduInstituteModel editibleAttributes);

    public Task<Fin<EducationInstitutionModel>> DeleteAsync();
}