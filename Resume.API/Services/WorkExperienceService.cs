using BeaniesUtilities.Models.Resume;
using Gay.TCazier.DatabaseParser.Data.Contexts;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;
using Gay.TCazier.DatabaseParser.Services.Interfaces;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;

namespace Gay.TCazier.DatabaseParser.Services;

public class WorkExperienceService : BaseModelService, IWorkExperienceService
{

    IServiceProvider _provider;

    public WorkExperienceService(IServiceProvider provider)
    {
        _provider = provider;
    }

    public async Task<Fin<WorkExperienceModel>> CreateAsync(EditibleWorkExperienceModel editibleAttributes)
    {
        var ctx = _provider.GetService<ResumeContext>();
        if (ctx == null)
        {
            return Error.New(new NullReferenceException("The provider returned a null DbContext while trying to create a new Address model"));
        }

        var entries = await ctx.WorkExperiences.ToListAsync();

        //check for base model parameter uniqueness

        //check for model uniqueness

        var model = new WorkExperienceModel()
        {
            StartDate = editibleAttributes.StartDate,
            EndDate = editibleAttributes.EndDate,
            Company = editibleAttributes.Company,
            Description = editibleAttributes.Description,
            Responsibilities = editibleAttributes.Responsibilities,
            TechUsed = editibleAttributes.TechUsed,

            Name = editibleAttributes.Name,
            IsHidden = editibleAttributes.IsHidden,

            CommonIdentity = GetNextCommonID(entries),
            CreatedBy = "Tiabeanie",
            CreatedOn = DateTime.UtcNow,
            Notes = "Entry Creation",
        };

        await ctx.WorkExperiences.AddAsync(model);
        await ctx.SaveChangesAsync();

        return model;
    }

    public async Task<Fin<WorkExperienceModel>> DeleteAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<WorkExperienceModel>>> GetAllAsync()
    {
        var ctx = _provider.GetService<ResumeContext>();
        var entries = await ctx.WorkExperiences.ToListAsync();
        return entries;
    }

    public async Task<Fin<IEnumerable<WorkExperienceModel>>> GetAllWithinEntryIDRangeAsync(int start, int end)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<WorkExperienceModel>>> GetAllWithinIDRangeAsync(int start, int end)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<WorkExperienceModel>> GetByEntryIDAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<WorkExperienceModel>> GetByIDAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<WorkExperienceModel>> GetHistroyOfIDAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<WorkExperienceModel>>> SearchBetweenDates(DateTime start, DateTime end)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<WorkExperienceModel>>> SearchBetweenModificationDatesAsync(DateTime start, DateTime end)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<WorkExperienceModel>>> SearchByCompanyAsync(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<WorkExperienceModel>>> SearchByIsDeletedAsync(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<WorkExperienceModel>>> SearchByIsHiddenAsync(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<WorkExperienceModel>>> SearchByNameAsync(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<WorkExperienceModel>>> SearchByNotesAsync(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<WorkExperienceModel>>> SearchByResponsibility(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<WorkExperienceModel>>> SearchByTechAsync(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<WorkExperienceModel>> UpdateAsync(EditibleWorkExperienceModel editibleAttributes)
    {
        throw new NotImplementedException();
    }
}