using BeaniesUtilities.Models.Resume;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;
using Gay.TCazier.DatabaseParser.Data.Contexts;
using LanguageExt;
using LanguageExt.Common;

namespace Gay.TCazier.DatabaseParser.Services.Interfaces;

public interface IEducationInstitutionService
{
    public Task<Fin<IEnumerable<EducationInstitutionModel>>> GetAllAsync(ResumeContext ctx);
    public Task<Fin<IEnumerable<EducationInstitutionModel>>> GetAllWithinIDRangeAsync(ResumeContext ctx, int start, int end);
    public Task<Fin<IEnumerable<EducationInstitutionModel>>> GetAllWithinEntryIDRangeAsync(ResumeContext ctx, int start, int end);
    public Task<Fin<EducationInstitutionModel>> GetByIDAsync(ResumeContext ctx, int id);
    public Task<Fin<EducationInstitutionModel>> GetByEntryIDAsync(ResumeContext ctx, int id);
    //public Task<Fin<EducationInstitutionModel>> GetHistroyOfIDAsync(ResumeContext ctx, int id);

    public Task<Fin<IEnumerable<EducationInstitutionModel>>> SearchByNameAsync(ResumeContext ctx, string searchTerm);
    public Task<Fin<IEnumerable<EducationInstitutionModel>>> SearchByNotesAsync(ResumeContext ctx, string searchTerm);
    public Task<Fin<IEnumerable<EducationInstitutionModel>>> SearchBetweenModificationDatesAsync(ResumeContext ctx, DateTime start, DateTime end);
    public Task<Fin<IEnumerable<EducationInstitutionModel>>> SearchByIsHiddenAsync(ResumeContext ctx, string searchTerm);
    public Task<Fin<IEnumerable<EducationInstitutionModel>>> SearchByIsDeletedAsync(ResumeContext ctx, string searchTerm);

    //ADD YOUR MODEL SPECIFIC QUERY SERVICE FUNCTIONS HERE

    public Task<Fin<EducationInstitutionModel>> CreateAsync(ResumeContext ctx, EditibleEducationInstitutionModel editibleAttributes);

    public Task<Fin<EducationInstitutionModel>> UpdateAsync(ResumeContext ctx, int id, EditibleEducationInstitutionModel editibleAttributes);

    public Task<Fin<IEnumerable<EducationInstitutionModel>>> DeleteAsync(ResumeContext ctx, int id);
    public Task<Fin<EducationInstitutionModel>> DeleteEntryAsync(ResumeContext ctx, int id);
}