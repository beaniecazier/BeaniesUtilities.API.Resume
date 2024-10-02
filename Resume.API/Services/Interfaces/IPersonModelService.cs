using BeaniesUtilities.Models.Resume;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;
using Gay.TCazier.DatabaseParser.Data.Contexts;
using LanguageExt;
using LanguageExt.Common;

namespace Gay.TCazier.DatabaseParser.Services.Interfaces;

public interface IPersonService
{
    public Task<Fin<IEnumerable<PersonModel>>> GetAllAsync(ResumeContext ctx);
    public Task<Fin<IEnumerable<PersonModel>>> GetAllWithinIDRangeAsync(ResumeContext ctx, int start, int end);
    public Task<Fin<IEnumerable<PersonModel>>> GetAllWithinEntryIDRangeAsync(ResumeContext ctx, int start, int end);
    public Task<Fin<PersonModel>> GetByIDAsync(ResumeContext ctx, int id);
    public Task<Fin<PersonModel>> GetByEntryIDAsync(ResumeContext ctx, int id);
    //public Task<Fin<PersonModel>> GetHistroyOfIDAsync(ResumeContext ctx, int id);

    public Task<Fin<IEnumerable<PersonModel>>> SearchByNameAsync(ResumeContext ctx, string searchTerm);
    public Task<Fin<IEnumerable<PersonModel>>> SearchByNotesAsync(ResumeContext ctx, string searchTerm);
    public Task<Fin<IEnumerable<PersonModel>>> SearchBetweenModificationDatesAsync(ResumeContext ctx, DateTime start, DateTime end);
    public Task<Fin<IEnumerable<PersonModel>>> SearchByIsHiddenAsync(ResumeContext ctx, string searchTerm);
    public Task<Fin<IEnumerable<PersonModel>>> SearchByIsDeletedAsync(ResumeContext ctx, string searchTerm);

    //ADD YOUR MODEL SPECIFIC QUERY SERVICE FUNCTIONS HERE

    public Task<Fin<PersonModel>> CreateAsync(ResumeContext ctx, EditiblePersonModel editibleAttributes);

    public Task<Fin<PersonModel>> UpdateAsync(ResumeContext ctx, int id, EditiblePersonModel editibleAttributes);

    public Task<Fin<IEnumerable<PersonModel>>> DeleteAsync(ResumeContext ctx, int id);
    public Task<Fin<PersonModel>> DeleteEntryAsync(ResumeContext ctx, int id);
}