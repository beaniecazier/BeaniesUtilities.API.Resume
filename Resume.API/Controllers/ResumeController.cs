using BeaniesUtilities.Models.Resume;
using Gay.TCazier.DatabaseParser.Data.Contexts;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gay.TCazier.DatabaseParser.Controllers;

public class ResumeController : Controller
{
    private readonly ResumeContext _context;

    public ResumeController(ResumeContext context)
    {
        _context = context;
    }

    //[HttpGet]
    //[ProducesResponseType(typeof(List<ResumeModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var resumes = await _context.People.Where(p => p.GetType() == typeof(ResumeModel)).ToListAsync();
        return Ok(resumes);
    }

    //[HttpGet("{id:int}")]
    //[ProducesResponseType(typeof(ResumeModel), StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var resume = await _context.People.Where(p => p.GetType() == typeof(ResumeModel))
            .SingleOrDefaultAsync(x => x.EntryIdentity == id);

        return resume == null ? NotFound() : Ok(resume);
    }

    //[HttpPost]
    //[ProducesResponseType(typeof(ResumeModel), StatusCodes.Status201Created)]
    //public async Task<IActionResult> Create([FromBody] EditibleResumeModel editibleAttributes)
    //{

    //    return CreatedAtAction(nameof(Get), new { id = model.EntryIdentity }, model);
    //}

    //[HttpPut("{id:int}")]
    //[ProducesResponseType(typeof(ResumeModel), StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] EditibleResumeModel editibleAttributes)
    {
        var existingEntry = await _context.People.FindAsync(id) as ResumeModel;

        if (existingEntry is null)
            return NotFound();

        existingEntry.HeroStatement = editibleAttributes.HeroStatement;
        existingEntry.Degrees = editibleAttributes.Degrees;
        existingEntry.Certificates = editibleAttributes.Certificates;
        existingEntry.Projects = editibleAttributes.Projects;
        existingEntry.WorkExperience = editibleAttributes.WorkExperience;

        existingEntry.PreferedName = editibleAttributes.PreferedName;
        existingEntry.Emails = editibleAttributes.Emails;
        existingEntry.Pronouns = editibleAttributes.Pronouns;
        existingEntry.Socials = editibleAttributes.Socials;
        existingEntry.Addresses = editibleAttributes.Addresses;
        existingEntry.PhoneNumbers = editibleAttributes.PhoneNumbers;

        existingEntry.Name = editibleAttributes.Name;
        existingEntry.IsHidden = editibleAttributes.IsHidden;

        existingEntry.ModifiedBy = "Tiabeanie";
        existingEntry.ModifiedOn = DateTime.UtcNow;
        existingEntry.Notes = "Editted";

        await _context.SaveChangesAsync();

        return Ok(existingEntry);
    }

    //[HttpDelete("{id:int}")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Remove([FromRoute] int id)
    {
        var existingEntry = await _context.People.FindAsync(id);

        if (existingEntry is null)
            return NotFound();

        _context.People.Remove(existingEntry);
        // ctx.Remove(existingMovie);
        // ctx.Movies.Remove( new Movie { Id = id });

        await _context.SaveChangesAsync();

        return Ok();
    }
}