using BeaniesUtilities.Models.Resume;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;
using LanguageExt;

namespace Gay.TCazier.DatabaseParser.Services.Interfaces;

public interface IPersonService
{
    public Task<Fin<IEnumerable<PersonModel>>> GetAllAsync();
    public Task<Fin<IEnumerable<PersonModel>>> GetAllWithinIDRangeAsync(int start, int end);
    public Task<Fin<IEnumerable<PersonModel>>> GetAllWithinEntryIDRangeAsync(int start, int end);
    public Task<Fin<PersonModel>> GetByIDAsync(int id);
    public Task<Fin<PersonModel>> GetByEntryIDAsync(int id);
    public Task<Fin<PersonModel>> GetHistroyOfIDAsync(int id);

    public Task<Fin<IEnumerable<PersonModel>>> SearchByNameAsync(string searchTerm);
    public Task<Fin<IEnumerable<PersonModel>>> SearchByNotesAsync(string searchTerm);
    public Task<Fin<IEnumerable<PersonModel>>> SearchBetweenModificationDatesAsync(DateTime start, DateTime end);
    public Task<Fin<IEnumerable<PersonModel>>> SearchByIsHiddenAsync(string searchTerm);
    public Task<Fin<IEnumerable<PersonModel>>> SearchByIsDeletedAsync(string searchTerm);

    public Task<Fin<IEnumerable<PersonModel>>> SearchBySocialsAsync(string searchTerm);
    public Task<Fin<IEnumerable<PersonModel>>> SearchByEmailAsync(string searchTerm);
    public Task<Fin<IEnumerable<PersonModel>>> SearchByPhoneNumberAsync(string searchTerm);
    public Task<Fin<IEnumerable<PersonModel>>> SearchByPreferedNameAsync(string searchTerm);

    public Task<Fin<PersonModel>> CreateAsync(EditiblePersonModel editibleAttributes);

    public Task<Fin<PersonModel>> UpdateAsync(EditiblePersonModel editibleAttributes);

    public Task<Fin<PersonModel>> DeleteAsync();
}