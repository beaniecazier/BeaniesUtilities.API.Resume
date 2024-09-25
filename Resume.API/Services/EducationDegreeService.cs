using BeaniesUtilities.Models.Resume;
using Gay.TCazier.DatabaseParser.Data.Contexts;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;
using Gay.TCazier.DatabaseParser.Services.Interfaces;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;

namespace Gay.TCazier.DatabaseParser.Services;

public class EducationDegreeService : BaseModelService, IEducationDegreeService
{

    IServiceProvider _provider;

    public EducationDegreeService(IServiceProvider provider)
    {
        _provider = provider;
    }

    public async Task<Fin<EducationDegreeModel>> CreateAsync(EditibleEduDegreeModel editibleAttributes)
    {
        var ctx = _provider.GetService<ResumeContext>();
        if (ctx == null)
        {
            return Error.New(new NullReferenceException("The provider returned a null DbContext while trying to create a new Address model"));
        }

        var entries = await ctx.Degrees.ToListAsync();

        //check for base model parameter uniqueness

        //check for model uniqueness

        var model = new EducationDegreeModel()
        {
            StartDate = editibleAttributes.StartDate,
            EndDate = editibleAttributes.EndDate,
            GPA = editibleAttributes.GPA,
            Institution = editibleAttributes.Institution,

            Name = editibleAttributes.Name,
            IsHidden = editibleAttributes.IsHidden,

            CommonIdentity = GetNextCommonID(entries),
            CreatedBy = "Tiabeanie",
            CreatedOn = DateTime.UtcNow,
            Notes = "Entry Creation",
        };

        await ctx.Degrees.AddAsync(model);
        await ctx.SaveChangesAsync();

        return model;
    }

    public async Task<Fin<EducationDegreeModel>> DeleteAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<EducationDegreeModel>>> GetAllAsync()
    {
        var ctx = _provider.GetService<ResumeContext>();
        var entries = await ctx.Degrees.ToListAsync();
        return entries;
    }

    public async Task<Fin<IEnumerable<EducationDegreeModel>>> GetAllWithinEntryIDRangeAsync(int start, int end)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<EducationDegreeModel>>> GetAllWithinIDRangeAsync(int start, int end)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<EducationDegreeModel>> GetByEntryIDAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<EducationDegreeModel>> GetByIDAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<EducationDegreeModel>> GetHistroyOfIDAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<EducationDegreeModel>>> SearchBetweenDates(DateTime start, DateTime end)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<EducationDegreeModel>>> SearchBetweenModificationDatesAsync(DateTime start, DateTime end)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<EducationDegreeModel>>> SearchByInstitute(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<EducationDegreeModel>>> SearchByIsDeletedAsync(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<EducationDegreeModel>>> SearchByIsHiddenAsync(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<EducationDegreeModel>>> SearchByNameAsync(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<EducationDegreeModel>>> SearchByNotesAsync(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<EducationDegreeModel>> UpdateAsync(EditibleEduDegreeModel editibleAttributes)
    {
        throw new NotImplementedException();
    }
}