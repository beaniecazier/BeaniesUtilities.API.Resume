using BeaniesUtilities.Models.Resume;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;
using LanguageExt;
using LanguageExt.Common;

namespace Gay.TCazier.DatabaseParser.Services.Interfaces;

public interface IAddressService
{
    public Task<Fin<IEnumerable<AddressModel>>> GetAllAsync();
    public Task<Fin<IEnumerable<AddressModel>>> GetAllWithinIDRangeAsync(int start, int end);
    public Task<Fin<IEnumerable<AddressModel>>> GetAllWithinEntryIDRangeAsync(int start, int end);
    public Task<Fin<AddressModel>> GetByIDAsync(int id);
    public Task<Fin<AddressModel>> GetByEntryIDAsync(int id);
    //public Task<Fin<AddressModel>> GetHistroyOfIDAsync(int id);

    public Task<Fin<IEnumerable<AddressModel>>> SearchByNameAsync(string searchTerm);
    public Task<Fin<IEnumerable<AddressModel>>> SearchByNotesAsync(string searchTerm);
    public Task<Fin<IEnumerable<AddressModel>>> SearchBetweenModificationDatesAsync(DateTime start, DateTime end);
    public Task<Fin<IEnumerable<AddressModel>>> SearchByIsHiddenAsync(string searchTerm);
    public Task<Fin<IEnumerable<AddressModel>>> SearchByIsDeletedAsync(string searchTerm);

    public Task<Fin<IEnumerable<AddressModel>>> SearchByStreetNameAsync(string searchTerm);
    public Task<Fin<IEnumerable<AddressModel>>> SearchByCountryAsync(string searchTerm);
    public Task<Fin<IEnumerable<AddressModel>>> SearchByStateAsync(string searchTerm);
    public Task<Fin<IEnumerable<AddressModel>>> SearchByZipAsync(string searchTerm);
    public Task<Fin<IEnumerable<AddressModel>>> SearchByCityAsync(string searchTerm);

    public Task<Fin<AddressModel>> CreateAsync(EditibleAddressModel editibleAttributes);

    public Task<Fin<AddressModel>> UpdateAsync(int id, EditibleAddressModel editibleAttributes);

    public Task<Fin<AddressModel>> DeleteAsync(int id);
}
