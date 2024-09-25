using BeaniesUtilities.Models.Resume;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;
using LanguageExt;

namespace Gay.TCazier.DatabaseParser.Services.Interfaces;

public interface IPhoneNumberService
{
    public Task<Fin<IEnumerable<PhoneNumberModel>>> GetAllAsync();
    public Task<Fin<IEnumerable<PhoneNumberModel>>> GetAllWithinIDRangeAsync(int start, int end);
    public Task<Fin<IEnumerable<PhoneNumberModel>>> GetAllWithinEntryIDRangeAsync(int start, int end);
    public Task<Fin<PhoneNumberModel>> GetByIDAsync(int id);
    public Task<Fin<PhoneNumberModel>> GetByEntryIDAsync(int id);
    public Task<Fin<PhoneNumberModel>> GetHistroyOfIDAsync(int id);

    public Task<Fin<IEnumerable<PhoneNumberModel>>> SearchByNameAsync(string searchTerm);
    public Task<Fin<IEnumerable<PhoneNumberModel>>> SearchByNotesAsync(string searchTerm);
    public Task<Fin<IEnumerable<PhoneNumberModel>>> SearchBetweenModificationDatesAsync(DateTime start, DateTime end);
    public Task<Fin<IEnumerable<PhoneNumberModel>>> SearchByIsHiddenAsync(string searchTerm);
    public Task<Fin<IEnumerable<PhoneNumberModel>>> SearchByIsDeletedAsync(string searchTerm);

    public Task<Fin<IEnumerable<PhoneNumberModel>>> SearchByCountryCodeAsync(string searchTerm);
    public Task<Fin<IEnumerable<PhoneNumberModel>>> SearchByAreaCodeAsync(string searchTerm);

    public Task<Fin<PhoneNumberModel>> CreateAsync(EditiblePhoneNumberModel editibleAttributes);

    public Task<Fin<PhoneNumberModel>> UpdateAsync(EditiblePhoneNumberModel editibleAttributes);

    public Task<Fin<PhoneNumberModel>> DeleteAsync();
}