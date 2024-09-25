using BeaniesUtilities.Models.Resume;
using Gay.TCazier.DatabaseParser.Data.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gay.TCazier.DatabaseParser.Controllers;

public class TechTagController : Controller
{
    private readonly ResumeContext _context;

    public TechTagController(ResumeContext context)
    {
        _context = context;
    }

    //[HttpGet]
    //[ProducesResponseType(typeof(List<TechTagModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _context.TechTags.ToListAsync());
    }

    //[HttpGet("{id:int}")]
    //[ProducesResponseType(typeof(TechTagModel), StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var experience = await _context.TechTags
            .SingleOrDefaultAsync(x => x.EntryIdentity == id);

        return experience == null ? NotFound() : Ok(experience);
    }

    //[HttpPost]
    //[ProducesResponseType(typeof(TechTagModel), StatusCodes.Status201Created)]
    //public async Task<IActionResult> Create([FromBody] EditibleTechTagModel editibleAttributes)
    //{

    //    return CreatedAtAction(nameof(Get), new { id = model.EntryIdentity }, model);
    //}

    //[HttpPut("{id:int}")]
    //[ProducesResponseType(typeof(TechTagModel), StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] TechTagModel editibleAttributes)
    {
        var existingEntry = await _context.TechTags.FindAsync(id);

        if (existingEntry is null)
            return NotFound();

        existingEntry.URL = editibleAttributes.URL;
        existingEntry.Description = editibleAttributes.Description;

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
        var existingMovie = await _context.TechTags.FindAsync(id);

        if (existingMovie is null)
            return NotFound();

        _context.TechTags.Remove(existingMovie);
        // ctx.Remove(existingMovie);
        // ctx.Movies.Remove( new Movie { Id = id });

        await _context.SaveChangesAsync();

        return Ok();
    }
}