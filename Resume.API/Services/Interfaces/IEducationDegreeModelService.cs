using BeaniesUtilities.Models.Resume;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;
using Gay.TCazier.DatabaseParser.Data.Contexts;
using LanguageExt;
using LanguageExt.Common;

namespace Gay.TCazier.DatabaseParser.Services.Interfaces;

public interface IEducationDegreeService
{
    public Task<Fin<IEnumerable<EducationDegreeModel>>> GetAllAsync(ResumeContext ctx);
    public Task<Fin<IEnumerable<EducationDegreeModel>>> GetAllWithinIDRangeAsync(ResumeContext ctx, int start, int end);
    public Task<Fin<IEnumerable<EducationDegreeModel>>> GetAllWithinEntryIDRangeAsync(ResumeContext ctx, int start, int end);
    public Task<Fin<EducationDegreeModel>> GetByIDAsync(ResumeContext ctx, int id);
    public Task<Fin<EducationDegreeModel>> GetByEntryIDAsync(ResumeContext ctx, int id);
    //public Task<Fin<EducationDegreeModel>> GetHistroyOfIDAsync(ResumeContext ctx, int id);

    public Task<Fin<IEnumerable<EducationDegreeModel>>> SearchByNameAsync(ResumeContext ctx, string searchTerm);
    public Task<Fin<IEnumerable<EducationDegreeModel>>> SearchByNotesAsync(ResumeContext ctx, string searchTerm);
    public Task<Fin<IEnumerable<EducationDegreeModel>>> SearchBetweenModificationDatesAsync(ResumeContext ctx, DateTime start, DateTime end);
    public Task<Fin<IEnumerable<EducationDegreeModel>>> SearchByIsHiddenAsync(ResumeContext ctx, string searchTerm);
    public Task<Fin<IEnumerable<EducationDegreeModel>>> SearchByIsDeletedAsync(ResumeContext ctx, string searchTerm);

    //ADD YOUR MODEL SPECIFIC QUERY SERVICE FUNCTIONS HERE

    public Task<Fin<EducationDegreeModel>> CreateAsync(ResumeContext ctx, EditibleEducationDegreeModel editibleAttributes);

    public Task<Fin<EducationDegreeModel>> UpdateAsync(ResumeContext ctx, int id, EditibleEducationDegreeModel editibleAttributes);

    public Task<Fin<IEnumerable<EducationDegreeModel>>> DeleteAsync(ResumeContext ctx, int id);
    public Task<Fin<EducationDegreeModel>> DeleteEntryAsync(ResumeContext ctx, int id);
}