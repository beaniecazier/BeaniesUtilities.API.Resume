using BeaniesUtilities.Models.Resume;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;
using Gay.TCazier.DatabaseParser.Data.Contexts;
using LanguageExt;
using LanguageExt.Common;

namespace Gay.TCazier.DatabaseParser.Services.Interfaces;

public interface IPhoneNumberService
{
    public Task<Fin<IEnumerable<PhoneNumberModel>>> GetAllAsync(ResumeContext ctx);
    public Task<Fin<IEnumerable<PhoneNumberModel>>> GetAllWithinIDRangeAsync(ResumeContext ctx, int start, int end);
    public Task<Fin<IEnumerable<PhoneNumberModel>>> GetAllWithinEntryIDRangeAsync(ResumeContext ctx, int start, int end);
    public Task<Fin<PhoneNumberModel>> GetByIDAsync(ResumeContext ctx, int id);
    public Task<Fin<PhoneNumberModel>> GetByEntryIDAsync(ResumeContext ctx, int id);
    //public Task<Fin<PhoneNumberModel>> GetHistroyOfIDAsync(ResumeContext ctx, int id);

    public Task<Fin<IEnumerable<PhoneNumberModel>>> SearchByNameAsync(ResumeContext ctx, string searchTerm);
    public Task<Fin<IEnumerable<PhoneNumberModel>>> SearchByNotesAsync(ResumeContext ctx, string searchTerm);
    public Task<Fin<IEnumerable<PhoneNumberModel>>> SearchBetweenModificationDatesAsync(ResumeContext ctx, DateTime start, DateTime end);
    public Task<Fin<IEnumerable<PhoneNumberModel>>> SearchByIsHiddenAsync(ResumeContext ctx, string searchTerm);
    public Task<Fin<IEnumerable<PhoneNumberModel>>> SearchByIsDeletedAsync(ResumeContext ctx, string searchTerm);

    //ADD YOUR MODEL SPECIFIC QUERY SERVICE FUNCTIONS HERE

    public Task<Fin<PhoneNumberModel>> CreateAsync(ResumeContext ctx, EditiblePhoneNumberModel editibleAttributes);

    public Task<Fin<PhoneNumberModel>> UpdateAsync(ResumeContext ctx, int id, EditiblePhoneNumberModel editibleAttributes);

    public Task<Fin<IEnumerable<PhoneNumberModel>>> DeleteAsync(ResumeContext ctx, int id);
    public Task<Fin<PhoneNumberModel>> DeleteEntryAsync(ResumeContext ctx, int id);
}