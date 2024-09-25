using BeaniesUtilities.Models.Resume;
using Gay.TCazier.DatabaseParser.Data.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gay.TCazier.DatabaseParser.Controllers;

public class PhoneNumberController : Controller
{
    private readonly ResumeContext _context;

    public PhoneNumberController(ResumeContext context)
    {
        _context = context;
    }


    //[HttpGet]
    //[ProducesResponseType(typeof(List<PhoneNumberModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _context.PhoneNumbers.ToListAsync());
    }

    //[HttpGet("{id:int}")]
    //[ProducesResponseType(typeof(PhoneNumberModel), StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var phoneNumber = await _context.PhoneNumbers
            .SingleOrDefaultAsync(x => x.EntryIdentity == id);

        return phoneNumber == null ? NotFound() : Ok(phoneNumber);
    }

    //[HttpPost]
    //[ProducesResponseType(typeof(PhoneNumberModel), StatusCodes.Status201Created)]
    //public async Task<IActionResult> Create([FromBody] EditiblePhoneNumberModel editibleAttributes)
    //{

    //    return CreatedAtAction(nameof(Get), new { id = model.EntryIdentity }, model);
    //}

    //[HttpPut("{id:int}")]
    //[ProducesResponseType(typeof(PhoneNumberModel), StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] PhoneNumberModel editibleAttributes)
    {
        var existingEntry = await _context.PhoneNumbers.FindAsync(id);

        if (existingEntry is null)
            return NotFound();

        existingEntry.CountryCode = editibleAttributes.CountryCode;
        existingEntry.AreaCode = editibleAttributes.AreaCode;
        existingEntry.TelephonePrefix = editibleAttributes.TelephonePrefix;
        existingEntry.LineNumber = editibleAttributes.LineNumber;

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
        var existingMovie = await _context.PhoneNumbers.FindAsync(id);

        if (existingMovie is null)
            return NotFound();

        _context.PhoneNumbers.Remove(existingMovie);
        // ctx.Remove(existingMovie);
        // ctx.Movies.Remove( new Movie { Id = id });

        await _context.SaveChangesAsync();

        return Ok();
    }
}