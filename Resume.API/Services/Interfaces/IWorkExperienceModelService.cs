using BeaniesUtilities.Models.Resume;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;
using Gay.TCazier.DatabaseParser.Data.Contexts;
using LanguageExt;
using LanguageExt.Common;

namespace Gay.TCazier.DatabaseParser.Services.Interfaces;

public interface IWorkExperienceService
{
    public Task<Fin<IEnumerable<WorkExperienceModel>>> GetAllAsync(ResumeContext ctx);
    public Task<Fin<IEnumerable<WorkExperienceModel>>> GetAllWithinIDRangeAsync(ResumeContext ctx, int start, int end);
    public Task<Fin<IEnumerable<WorkExperienceModel>>> GetAllWithinEntryIDRangeAsync(ResumeContext ctx, int start, int end);
    public Task<Fin<WorkExperienceModel>> GetByIDAsync(ResumeContext ctx, int id);
    public Task<Fin<WorkExperienceModel>> GetByEntryIDAsync(ResumeContext ctx, int id);
    //public Task<Fin<WorkExperienceModel>> GetHistroyOfIDAsync(ResumeContext ctx, int id);

    public Task<Fin<IEnumerable<WorkExperienceModel>>> SearchByNameAsync(ResumeContext ctx, string searchTerm);
    public Task<Fin<IEnumerable<WorkExperienceModel>>> SearchByNotesAsync(ResumeContext ctx, string searchTerm);
    public Task<Fin<IEnumerable<WorkExperienceModel>>> SearchBetweenModificationDatesAsync(ResumeContext ctx, DateTime start, DateTime end);
    public Task<Fin<IEnumerable<WorkExperienceModel>>> SearchByIsHiddenAsync(ResumeContext ctx, string searchTerm);
    public Task<Fin<IEnumerable<WorkExperienceModel>>> SearchByIsDeletedAsync(ResumeContext ctx, string searchTerm);

    //ADD YOUR MODEL SPECIFIC QUERY SERVICE FUNCTIONS HERE

    public Task<Fin<WorkExperienceModel>> CreateAsync(ResumeContext ctx, EditibleWorkExperienceModel editibleAttributes);

    public Task<Fin<WorkExperienceModel>> UpdateAsync(ResumeContext ctx, int id, EditibleWorkExperienceModel editibleAttributes);

    public Task<Fin<IEnumerable<WorkExperienceModel>>> DeleteAsync(ResumeContext ctx, int id);
    public Task<Fin<WorkExperienceModel>> DeleteEntryAsync(ResumeContext ctx, int id);
}