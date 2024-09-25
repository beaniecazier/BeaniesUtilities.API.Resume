using BeaniesUtilities.Models.Resume;
using Gay.TCazier.DatabaseParser.Data.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gay.TCazier.DatabaseParser.Controllers;

public class CertificateController : Controller
{
    private readonly ResumeContext _context;

    public CertificateController(ResumeContext context)
    {
        _context = context;
    }


    //[HttpGet]
    //[ProducesResponseType(typeof(List<CertificateModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _context.Certificates.ToListAsync());
    }

    //[HttpGet("{id:int}")]
    //[ProducesResponseType(typeof(CertificateModel), StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var experience = await _context.Certificates
            .SingleOrDefaultAsync(x => x.EntryIdentity == id);

        return experience == null ? NotFound() : Ok(experience);
    }

    //[HttpPost]
    //[ProducesResponseType(typeof(CertificateModel), StatusCodes.Status201Created)]
    //public async Task<IActionResult> Create([FromBody] EditibleCertificateModel editibleAttributes)
    //{

    //    return CreatedAtAction(nameof(Get), new { id = model.EntryIdentity }, model);
    //}

    //[HttpPut("{id:int}")]
    //[ProducesResponseType(typeof(CertificateModel), StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CertificateModel editibleAttributes)
    {
        var existingEntry = await _context.Certificates.FindAsync(id);

        if (existingEntry is null)
            return NotFound();

        existingEntry.IssueDate = editibleAttributes.IssueDate;
        existingEntry.Link = editibleAttributes.Link;
        existingEntry.PdfFileName = editibleAttributes.PdfFileName;
        existingEntry.Issuer = editibleAttributes.Issuer;

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
        var existingMovie = await _context.Certificates.FindAsync(id);

        if (existingMovie is null)
            return NotFound();

        _context.Certificates.Remove(existingMovie);
        // ctx.Remove(existingMovie);
        // ctx.Movies.Remove( new Movie { Id = id });

        await _context.SaveChangesAsync();

        return Ok();
    }
}