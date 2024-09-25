using BeaniesUtilities.Models.Resume;
using Gay.TCazier.DatabaseParser.Data.Contexts;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;
using Gay.TCazier.DatabaseParser.Services.Interfaces;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;

namespace Gay.TCazier.DatabaseParser.Services;

public class ResumeModelService : BaseModelService, IResumeService
{

    IServiceProvider _provider;

    public ResumeModelService(IServiceProvider provider)
    {
        _provider = provider;
    }

    public async Task<Fin<ResumeModel>> CreateAsync(EditibleResumeModel editibleAttributes)
    {
        var ctx = _provider.GetService<ResumeContext>();
        if (ctx == null)
        {
            return Error.New(new NullReferenceException("The provider returned a null DbContext while trying to create a new Address model"));
        }

        var entries = await ctx.People.ToListAsync();

        //check for base model parameter uniqueness

        //check for model uniqueness

        var model = new ResumeModel()
        {
            HeroStatement = editibleAttributes.HeroStatement,
            Degrees = editibleAttributes.Degrees,
            Certificates = editibleAttributes.Certificates,
            Projects = editibleAttributes.Projects,
            WorkExperience = editibleAttributes.WorkExperience,

            PreferedName = editibleAttributes.PreferedName,
            Pronouns = editibleAttributes.Pronouns,
            Emails = editibleAttributes.Emails,
            Socials = editibleAttributes.Socials,
            Addresses = editibleAttributes.Addresses,
            PhoneNumbers = editibleAttributes.PhoneNumbers,

            Name = editibleAttributes.Name,
            IsHidden = editibleAttributes.IsHidden,

            CommonIdentity = GetNextCommonID(entries),
            CreatedBy = "Tiabeanie",
            CreatedOn = DateTime.UtcNow,
            Notes = "Entry Creation",
        };

        await ctx.People.AddAsync(model);
        await ctx.SaveChangesAsync();

        return model;
    }

    public async Task<Fin<ResumeModel>> DeleteAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<ResumeModel>>> GetAllAsync()
    {
        var ctx = _provider.GetService<ResumeContext>();
        var entries = await ctx.People.Where(p => p.GetType() == typeof(ResumeModel)).Select(r => r as ResumeModel).ToListAsync();
        return entries;
    }

    public async Task<Fin<IEnumerable<ResumeModel>>> GetAllWithinEntryIDRangeAsync(int start, int end)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<ResumeModel>>> GetAllWithinIDRangeAsync(int start, int end)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<ResumeModel>> GetByEntryIDAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<ResumeModel>> GetByIDAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<ResumeModel>> GetHistroyOfIDAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<ResumeModel>>> SearchBetweenModificationDatesAsync(DateTime start, DateTime end)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<ResumeModel>>> SearchByEmailAsync(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<ResumeModel>>> SearchByIsDeletedAsync(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<ResumeModel>>> SearchByIsHiddenAsync(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<ResumeModel>>> SearchByNameAsync(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<ResumeModel>>> SearchByNotesAsync(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<ResumeModel>>> SearchByPhoneNumberAsync(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<ResumeModel>>> SearchByPreferedNameAsync(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<IEnumerable<ResumeModel>>> SearchBySocialsAsync(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public async Task<Fin<ResumeModel>> UpdateAsync(EditibleResumeModel editibleAttributes)
    {
        throw new NotImplementedException();
    }
}
