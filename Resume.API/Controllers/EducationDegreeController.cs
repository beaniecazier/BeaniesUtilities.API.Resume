using BeaniesUtilities.Models.Resume;
using Gay.TCazier.DatabaseParser.Data.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gay.TCazier.DatabaseParser.Controllers;

public class EducationDegreeController : Controller
{
    private readonly ResumeContext _context;

    public EducationDegreeController(ResumeContext context)
    {
        _context = context;
    }

    //[HttpGet]
    //[ProducesResponseType(typeof(List<EducationDegreeModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _context.Degrees.ToListAsync());
    }

    //[HttpGet("{id:int}")]
    //[ProducesResponseType(typeof(EducationDegreeModel), StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var education = await _context.Degrees
            .SingleOrDefaultAsync(x => x.EntryIdentity == id);

        return education == null ? NotFound() : Ok(education);
    }

    //[HttpPost]
    //[ProducesResponseType(typeof(EducationDegreeModel), StatusCodes.Status201Created)]
    //public async Task<IActionResult> Create([FromBody] EditibleEduDegreeModel editibleAttributes)
    //{

    //    return CreatedAtAction(nameof(Get), new { id = model.EntryIdentity }, model);
    //}

    //[HttpPut("{id:int}")]
    //[ProducesResponseType(typeof(EducationDegreeModel), StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] EducationDegreeModel editibleAttributes)
    {
        var existingEntry = await _context.Degrees.FindAsync(id);

        if (existingEntry is null)
            return NotFound();

        existingEntry.StartDate = editibleAttributes.StartDate;
        existingEntry.EndDate = editibleAttributes.EndDate;
        existingEntry.GPA = editibleAttributes.GPA;
        existingEntry.Institution = editibleAttributes.Institution;

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
        var existingMovie = await _context.Degrees.FindAsync(id);

        if (existingMovie is null)
            return NotFound();

        _context.Degrees.Remove(existingMovie);
        // ctx.Remove(existingMovie);
        // ctx.Movies.Remove( new Movie { Id = id });

        await _context.SaveChangesAsync();

        return Ok();
    }
}