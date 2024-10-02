using BeaniesUtilities.Models.Resume;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;
using Gay.TCazier.DatabaseParser.Data.Contexts;
using LanguageExt;
using LanguageExt.Common;

namespace Gay.TCazier.DatabaseParser.Services.Interfaces;

public interface IResumeService
{
    public Task<Fin<IEnumerable<ResumeModel>>> GetAllAsync(ResumeContext ctx);
    public Task<Fin<IEnumerable<ResumeModel>>> GetAllWithinIDRangeAsync(ResumeContext ctx, int start, int end);
    public Task<Fin<IEnumerable<ResumeModel>>> GetAllWithinEntryIDRangeAsync(ResumeContext ctx, int start, int end);
    public Task<Fin<ResumeModel>> GetByIDAsync(ResumeContext ctx, int id);
    public Task<Fin<ResumeModel>> GetByEntryIDAsync(ResumeContext ctx, int id);
    //public Task<Fin<ResumeModel>> GetHistroyOfIDAsync(ResumeContext ctx, int id);

    public Task<Fin<IEnumerable<ResumeModel>>> SearchByNameAsync(ResumeContext ctx, string searchTerm);
    public Task<Fin<IEnumerable<ResumeModel>>> SearchByNotesAsync(ResumeContext ctx, string searchTerm);
    public Task<Fin<IEnumerable<ResumeModel>>> SearchBetweenModificationDatesAsync(ResumeContext ctx, DateTime start, DateTime end);
    public Task<Fin<IEnumerable<ResumeModel>>> SearchByIsHiddenAsync(ResumeContext ctx, string searchTerm);
    public Task<Fin<IEnumerable<ResumeModel>>> SearchByIsDeletedAsync(ResumeContext ctx, string searchTerm);

    //ADD YOUR MODEL SPECIFIC QUERY SERVICE FUNCTIONS HERE

    public Task<Fin<ResumeModel>> CreateAsync(ResumeContext ctx, EditibleResumeModel editibleAttributes);

    public Task<Fin<ResumeModel>> UpdateAsync(ResumeContext ctx, int id, EditibleResumeModel editibleAttributes);

    public Task<Fin<IEnumerable<ResumeModel>>> DeleteAsync(ResumeContext ctx, int id);
    public Task<Fin<ResumeModel>> DeleteEntryAsync(ResumeContext ctx, int id);
}