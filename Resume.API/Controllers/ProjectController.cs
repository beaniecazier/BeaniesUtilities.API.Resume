using BeaniesUtilities.Models.Resume;
using Gay.TCazier.DatabaseParser.Data.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gay.TCazier.DatabaseParser.Controllers;

public class ProjectController : Controller
{
    private readonly ResumeContext _context;

    public ProjectController(ResumeContext context)
    {
        _context = context;
    }

    //[HttpGet]
    //[ProducesResponseType(typeof(List<ProjectModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _context.Projects.ToListAsync());
    }

    //[HttpGet("{id:int}")]
    //[ProducesResponseType(typeof(ProjectModel), StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var experience = await _context.Projects
            .SingleOrDefaultAsync(x => x.EntryIdentity == id);

        return experience == null ? NotFound() : Ok(experience);
    }

    //[HttpPost]
    //[ProducesResponseType(typeof(ProjectModel), StatusCodes.Status201Created)]
    //public async Task<IActionResult> Create([FromBody] EditibleProjectModel editibleAttributes)
    //{

    //    return CreatedAtAction(nameof(Get), new { id = model.EntryIdentity }, model);
    //}

    //[HttpPut("{id:int}")]
    //[ProducesResponseType(typeof(ProjectModel), StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ProjectModel editibleAttributes)
    {
        var existingEntry = await _context.Projects.FindAsync(id);

        if (existingEntry is null)
            return NotFound();

        existingEntry.Description = editibleAttributes.Description;
        existingEntry.Version = editibleAttributes.Version;
        existingEntry.ProjectUrl = editibleAttributes.ProjectUrl;
        existingEntry.StartDate = editibleAttributes.StartDate;
        existingEntry.CompletionDate = editibleAttributes.CompletionDate;
        existingEntry.TechTags = editibleAttributes.TechTags;

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
        var existingMovie = await _context.Projects.FindAsync(id);

        if (existingMovie is null)
            return NotFound();

        _context.Projects.Remove(existingMovie);
        // ctx.Remove(existingMovie);
        // ctx.Movies.Remove( new Movie { Id = id });

        await _context.SaveChangesAsync();

        return Ok();
    }
}

