using BeaniesUtilities.Models.Resume;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;
using Gay.TCazier.DatabaseParser.Data.Contexts;
using LanguageExt;
using LanguageExt.Common;

namespace Gay.TCazier.DatabaseParser.Services.Interfaces;

public interface ITechTagService
{
    public Task<Fin<IEnumerable<TechTagModel>>> GetAllAsync(ResumeContext ctx);
    public Task<Fin<IEnumerable<TechTagModel>>> GetAllWithinIDRangeAsync(ResumeContext ctx, int start, int end);
    public Task<Fin<IEnumerable<TechTagModel>>> GetAllWithinEntryIDRangeAsync(ResumeContext ctx, int start, int end);
    public Task<Fin<TechTagModel>> GetByIDAsync(ResumeContext ctx, int id);
    public Task<Fin<TechTagModel>> GetByEntryIDAsync(ResumeContext ctx, int id);
    //public Task<Fin<TechTagModel>> GetHistroyOfIDAsync(ResumeContext ctx, int id);

    public Task<Fin<IEnumerable<TechTagModel>>> SearchByNameAsync(ResumeContext ctx, string searchTerm);
    public Task<Fin<IEnumerable<TechTagModel>>> SearchByNotesAsync(ResumeContext ctx, string searchTerm);
    public Task<Fin<IEnumerable<TechTagModel>>> SearchBetweenModificationDatesAsync(ResumeContext ctx, DateTime start, DateTime end);
    public Task<Fin<IEnumerable<TechTagModel>>> SearchByIsHiddenAsync(ResumeContext ctx, string searchTerm);
    public Task<Fin<IEnumerable<TechTagModel>>> SearchByIsDeletedAsync(ResumeContext ctx, string searchTerm);

    //ADD YOUR MODEL SPECIFIC QUERY SERVICE FUNCTIONS HERE

    public Task<Fin<TechTagModel>> CreateAsync(ResumeContext ctx, EditibleTechTagModel editibleAttributes);

    public Task<Fin<TechTagModel>> UpdateAsync(ResumeContext ctx, int id, EditibleTechTagModel editibleAttributes);

    public Task<Fin<IEnumerable<TechTagModel>>> DeleteAsync(ResumeContext ctx, int id);
    public Task<Fin<TechTagModel>> DeleteEntryAsync(ResumeContext ctx, int id);
}