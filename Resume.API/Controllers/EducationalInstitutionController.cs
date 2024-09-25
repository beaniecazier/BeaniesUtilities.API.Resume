using BeaniesUtilities.Models.Resume;
using Gay.TCazier.DatabaseParser.Data.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gay.TCazier.DatabaseParser.Controllers;

public class EducationalInstitutionController : Controller
{
    private readonly ResumeContext _context;

    public EducationalInstitutionController(ResumeContext context)
    {
        _context = context;
    }

    //[HttpGet]
    //[ProducesResponseType(typeof(List<EducationInstitutionModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _context.Institutions.ToListAsync());
    }

    //[HttpGet("{id:int}")]
    ////[ProducesResponseType(typeof(EducationInstitutionModel), StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var experience = await _context.Institutions
            .SingleOrDefaultAsync(x => x.EntryIdentity == id);

        return experience == null ? NotFound() : Ok(experience);
    }

    //[HttpPost]
    //[ProducesResponseType(typeof(EducationInstitutionModel), StatusCodes.Status201Created)]
    //public async Task<IActionResult> Create([FromBody] EditibleEduInstituteModel editibleAttributes)
    //{

    //    return CreatedAtAction(nameof(Get), new { id = model.EntryIdentity }, model);
    //}

    //[HttpPut("{id:int}")]
    //[ProducesResponseType(typeof(EducationInstitutionModel), StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] EducationInstitutionModel editibleAttributes)
    {
        var existingEntry = await _context.Institutions.FindAsync(id);

        if (existingEntry is null)
            return NotFound();

        existingEntry.Website = editibleAttributes.Website;
        existingEntry.Address = editibleAttributes.Address;
        existingEntry.CertificatesIssued = editibleAttributes.CertificatesIssued;
        existingEntry.DegreesGiven = editibleAttributes.DegreesGiven;

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
        var existingMovie = await _context.Institutions.FindAsync(id);

        if (existingMovie is null)
            return NotFound();

        _context.Institutions.Remove(existingMovie);
        // ctx.Remove(existingMovie);
        // ctx.Movies.Remove( new Movie { Id = id });

        await _context.SaveChangesAsync();

        return Ok();
    }
}