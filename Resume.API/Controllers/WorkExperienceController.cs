using Gay.TCazier.DatabaseParser.Data.Contexts;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gay.TCazier.DatabaseParser.Controllers;

public class WorkExperienceController : Controller
{
    private readonly ResumeContext _context;

    public WorkExperienceController(ResumeContext context)
    {
        _context = context;
    }

    //[HttpGet]
    //[ProducesResponseType(typeof(List<WorkExperienceModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _context.WorkExperiences.ToListAsync());
    }

    //[HttpGet("{id:int}")]
    ////[ProducesResponseType(typeof(WorkExperienceModel), StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var experience = await _context.WorkExperiences
            .SingleOrDefaultAsync(x => x.EntryIdentity == id);

        return experience == null ? NotFound() : Ok(experience);
    }

    //[HttpPost]
    //[ProducesResponseType(typeof(WorkExperienceModel), StatusCodes.Status201Created)]
    //public async Task<IActionResult> Create([FromBody] EditibleWorkExperienceModel editibleAttributes)
    //{

    //    return CreatedAtAction(nameof(Get), new { id = model.EntryIdentity }, model);
    //}

    //[HttpPut("{id:int}")]
    //[ProducesResponseType(typeof(WorkExperienceModel), StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] EditibleWorkExperienceModel editibleAttributes)
    {
        var existingEntry = await _context.WorkExperiences.FindAsync(id);

        if (existingEntry is null)
            return NotFound();

        existingEntry.StartDate = editibleAttributes.StartDate;
        existingEntry.EndDate = editibleAttributes.EndDate;
        existingEntry.Company = editibleAttributes.Company;
        existingEntry.Description = editibleAttributes.Description;
        existingEntry.Responsibilities = editibleAttributes.Responsibilities;
        existingEntry.TechUsed = editibleAttributes.TechUsed;

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
        var existingMovie = await _context.WorkExperiences.FindAsync(id);

        if (existingMovie is null)
            return NotFound();

        _context.WorkExperiences.Remove(existingMovie);
        // ctx.Remove(existingMovie);
        // ctx.Movies.Remove( new Movie { Id = id });

        await _context.SaveChangesAsync();

        return Ok();
    }
}

