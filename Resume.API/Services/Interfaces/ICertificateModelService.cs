using BeaniesUtilities.Models.Resume;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;
using Gay.TCazier.DatabaseParser.Data.Contexts;
using LanguageExt;
using LanguageExt.Common;

namespace Gay.TCazier.DatabaseParser.Services.Interfaces;

public interface ICertificateService
{
    public Task<Fin<IEnumerable<CertificateModel>>> GetAllAsync(ResumeContext ctx);
    public Task<Fin<IEnumerable<CertificateModel>>> GetAllWithinIDRangeAsync(ResumeContext ctx, int start, int end);
    public Task<Fin<IEnumerable<CertificateModel>>> GetAllWithinEntryIDRangeAsync(ResumeContext ctx, int start, int end);
    public Task<Fin<CertificateModel>> GetByIDAsync(ResumeContext ctx, int id);
    public Task<Fin<CertificateModel>> GetByEntryIDAsync(ResumeContext ctx, int id);
    //public Task<Fin<CertificateModel>> GetHistroyOfIDAsync(ResumeContext ctx, int id);

    public Task<Fin<IEnumerable<CertificateModel>>> SearchByNameAsync(ResumeContext ctx, string searchTerm);
    public Task<Fin<IEnumerable<CertificateModel>>> SearchByNotesAsync(ResumeContext ctx, string searchTerm);
    public Task<Fin<IEnumerable<CertificateModel>>> SearchBetweenModificationDatesAsync(ResumeContext ctx, DateTime start, DateTime end);
    public Task<Fin<IEnumerable<CertificateModel>>> SearchByIsHiddenAsync(ResumeContext ctx, string searchTerm);
    public Task<Fin<IEnumerable<CertificateModel>>> SearchByIsDeletedAsync(ResumeContext ctx, string searchTerm);

    //ADD YOUR MODEL SPECIFIC QUERY SERVICE FUNCTIONS HERE

    public Task<Fin<CertificateModel>> CreateAsync(ResumeContext ctx, EditibleCertificateModel editibleAttributes);

    public Task<Fin<CertificateModel>> UpdateAsync(ResumeContext ctx, int id, EditibleCertificateModel editibleAttributes);

    public Task<Fin<IEnumerable<CertificateModel>>> DeleteAsync(ResumeContext ctx, int id);
    public Task<Fin<CertificateModel>> DeleteEntryAsync(ResumeContext ctx, int id);
}