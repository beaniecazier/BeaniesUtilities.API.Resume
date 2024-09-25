using BeaniesUtilities.Models.Resume;
using Gay.TCazier.DatabaseParser.Data.Contexts;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;
using Gay.TCazier.DatabaseParser.Services.Interfaces;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;

namespace Gay.TCazier.DatabaseParser.Services;

public class TechTagService : BaseModelService, ITechTagService
{

    IServiceProvider _provider;

    public TechTagService(IServiceProvider provider)
    {
        _provider = provider;
    }

    public async Task<Fin<TechTagModel>> CreateAsync(EditibleTechTagModel editibleAttributes)
    {
        var ctx = _provider.GetService<ResumeContext>();
        if (ctx == null)
        {
            return Error.New(new NullReferenceException("The provider returned a null DbContext while trying to create a new Address model"));
        }

        var entries = await ctx.TechTags.ToListAsync();

        //check for base model parameter uniqueness

        //check for model uniqueness

        var model = new TechTagModel()
        {
            URL = editibleAttributes.URL,
            Description = editibleAttributes.Description,

            Name = editibleAttributes.Name,
            IsHidden = editibleAttributes.IsHidden,

            CommonIdentity = GetNextCommonID(entries),
            CreatedBy = "Tiabeanie",
            CreatedOn = DateTime.UtcNow,
            Notes = "Entry Creation",
        };

        await ctx.TechTags.AddAsync(model);
        await ctx.SaveChangesAsync();

        return model;
    }

    public async Task<Fin<TechTagModel>> DeleteAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<TechTagModel>>> GetAllAsync()
    {
        var ctx = _provider.GetService<ResumeContext>();
        var entries = await ctx.TechTags.ToListAsync();
        return entries;
    }

    public async Task<Fin<IEnumerable<TechTagModel>>> GetAllWithinEntryIDRangeAsync(int start, int end)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<TechTagModel>>> GetAllWithinIDRangeAsync(int start, int end)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<TechTagModel>> GetByEntryIDAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<TechTagModel>> GetByIDAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<TechTagModel>> GetHistroyOfIDAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<TechTagModel>>> SearchBetweenModificationDatesAsync(DateTime start, DateTime end)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<TechTagModel>>> SearchByDescriptionAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<TechTagModel>>> SearchByIsDeletedAsync(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<TechTagModel>>> SearchByIsHiddenAsync(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<TechTagModel>>> SearchByNameAsync(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<TechTagModel>>> SearchByNotesAsync(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<TechTagModel>> UpdateAsync(EditibleTechTagModel editibleAttributes)
    {
        throw new NotImplementedException();
    }
}
