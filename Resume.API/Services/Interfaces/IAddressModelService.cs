using BeaniesUtilities.Models.Resume;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;
using Gay.TCazier.DatabaseParser.Data.Contexts;
using LanguageExt;
using LanguageExt.Common;

namespace Gay.TCazier.DatabaseParser.Services.Interfaces;

public interface IAddressService
{
    public Task<Fin<IEnumerable<AddressModel>>> GetAllAsync(ResumeContext ctx);
    public Task<Fin<IEnumerable<AddressModel>>> GetAllWithinIDRangeAsync(ResumeContext ctx, int start, int end);
    public Task<Fin<IEnumerable<AddressModel>>> GetAllWithinEntryIDRangeAsync(ResumeContext ctx, int start, int end);
    public Task<Fin<AddressModel>> GetByIDAsync(ResumeContext ctx, int id);
    public Task<Fin<AddressModel>> GetByEntryIDAsync(ResumeContext ctx, int id);
    //public Task<Fin<AddressModel>> GetHistroyOfIDAsync(ResumeContext ctx, int id);

    public Task<Fin<IEnumerable<AddressModel>>> SearchByNameAsync(ResumeContext ctx, string searchTerm);
    public Task<Fin<IEnumerable<AddressModel>>> SearchByNotesAsync(ResumeContext ctx, string searchTerm);
    public Task<Fin<IEnumerable<AddressModel>>> SearchBetweenModificationDatesAsync(ResumeContext ctx, DateTime start, DateTime end);
    public Task<Fin<IEnumerable<AddressModel>>> SearchByIsHiddenAsync(ResumeContext ctx, string searchTerm);
    public Task<Fin<IEnumerable<AddressModel>>> SearchByIsDeletedAsync(ResumeContext ctx, string searchTerm);

    //ADD YOUR MODEL SPECIFIC QUERY SERVICE FUNCTIONS HERE

    public Task<Fin<AddressModel>> CreateAsync(ResumeContext ctx, EditibleAddressModel editibleAttributes);

    public Task<Fin<AddressModel>> UpdateAsync(ResumeContext ctx, int id, EditibleAddressModel editibleAttributes);

    public Task<Fin<IEnumerable<AddressModel>>> DeleteAsync(ResumeContext ctx, int id);
    public Task<Fin<AddressModel>> DeleteEntryAsync(ResumeContext ctx, int id);
}