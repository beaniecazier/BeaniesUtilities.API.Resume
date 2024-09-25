using BeaniesUtilities.Models.Resume;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;
using LanguageExt;

namespace Gay.TCazier.DatabaseParser.Services.Interfaces;

public interface ICertificateService
{
    public Task<Fin<IEnumerable<CertificateModel>>> GetAllAsync();
    public Task<Fin<IEnumerable<CertificateModel>>> GetAllWithinIDRangeAsync(int start, int end);
    public Task<Fin<IEnumerable<CertificateModel>>> GetAllWithinEntryIDRangeAsync(int start, int end);
    public Task<Fin<CertificateModel>> GetByIDAsync(int id);
    public Task<Fin<CertificateModel>> GetByEntryIDAsync(int id);
    public Task<Fin<CertificateModel>> GetHistroyOfIDAsync(int id);

    public Task<Fin<IEnumerable<CertificateModel>>> SearchByNameAsync(string searchTerm);
    public Task<Fin<IEnumerable<CertificateModel>>> SearchByNotesAsync(string searchTerm);
    public Task<Fin<IEnumerable<CertificateModel>>> SearchBetweenModificationDatesAsync(DateTime start, DateTime end);
    public Task<Fin<IEnumerable<CertificateModel>>> SearchByIsHiddenAsync(string searchTerm);
    public Task<Fin<IEnumerable<CertificateModel>>> SearchByIsDeletedAsync(string searchTerm);

    public Task<Fin<IEnumerable<CertificateModel>>> SearchByCityIssuer(string searchTerm);
    public Task<Fin<IEnumerable<CertificateModel>>> SearchBetweenDates(DateTime start, DateTime end);

    public Task<Fin<CertificateModel>> CreateAsync(EditibleCertificateModel editibleAttributes);

    public Task<Fin<CertificateModel>> UpdateAsync(EditibleCertificateModel editibleAttributes);

    public Task<Fin<CertificateModel>> DeleteAsync();
}