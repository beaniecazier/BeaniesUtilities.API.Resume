using BeaniesUtilities.Models.Resume;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;
using Gay.TCazier.DatabaseParser.Data.Contexts;
using LanguageExt;
using LanguageExt.Common;

namespace Gay.TCazier.DatabaseParser.Services.Interfaces;

public interface IProjectService
{
    public Task<Fin<IEnumerable<ProjectModel>>> GetAllAsync(ResumeContext ctx);
    public Task<Fin<IEnumerable<ProjectModel>>> GetAllWithinIDRangeAsync(ResumeContext ctx, int start, int end);
    public Task<Fin<IEnumerable<ProjectModel>>> GetAllWithinEntryIDRangeAsync(ResumeContext ctx, int start, int end);
    public Task<Fin<ProjectModel>> GetByIDAsync(ResumeContext ctx, int id);
    public Task<Fin<ProjectModel>> GetByEntryIDAsync(ResumeContext ctx, int id);
    //public Task<Fin<ProjectModel>> GetHistroyOfIDAsync(ResumeContext ctx, int id);

    public Task<Fin<IEnumerable<ProjectModel>>> SearchByNameAsync(ResumeContext ctx, string searchTerm);
    public Task<Fin<IEnumerable<ProjectModel>>> SearchByNotesAsync(ResumeContext ctx, string searchTerm);
    public Task<Fin<IEnumerable<ProjectModel>>> SearchBetweenModificationDatesAsync(ResumeContext ctx, DateTime start, DateTime end);
    public Task<Fin<IEnumerable<ProjectModel>>> SearchByIsHiddenAsync(ResumeContext ctx, string searchTerm);
    public Task<Fin<IEnumerable<ProjectModel>>> SearchByIsDeletedAsync(ResumeContext ctx, string searchTerm);

    //ADD YOUR MODEL SPECIFIC QUERY SERVICE FUNCTIONS HERE

    public Task<Fin<ProjectModel>> CreateAsync(ResumeContext ctx, EditibleProjectModel editibleAttributes);

    public Task<Fin<ProjectModel>> UpdateAsync(ResumeContext ctx, int id, EditibleProjectModel editibleAttributes);

    public Task<Fin<IEnumerable<ProjectModel>>> DeleteAsync(ResumeContext ctx, int id);
    public Task<Fin<ProjectModel>> DeleteEntryAsync(ResumeContext ctx, int id);
}