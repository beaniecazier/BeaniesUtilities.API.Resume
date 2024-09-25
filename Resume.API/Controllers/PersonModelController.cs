using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;
using Gay.TCazier.DatabaseParser.Data.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gay.TCazier.DatabaseParser.Controllers;

public class PersonModelController : Controller
{
    private readonly ResumeContext _context;

    public PersonModelController(ResumeContext context)
    {
        _context = context;
    }

    //[HttpGet]
    //[ProducesResponseType(typeof(List<PersonModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _context.People.ToListAsync());
    }

    //[HttpGet("{id:int}")]
    //[ProducesResponseType(typeof(PersonModel), StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var experience = await _context.People
            .SingleOrDefaultAsync(x => x.EntryIdentity == id);

        return experience == null ? NotFound() : Ok(experience);
    }

    //[HttpPost]
    //[ProducesResponseType(typeof(PersonModel), StatusCodes.Status201Created)]
    //public async Task<IActionResult> Create([FromBody] EditiblePersonModel person)
    //{

    //    return CreatedAtAction(nameof(Get), new { id = model.EntryIdentity }, model);
    //}

    //[HttpPut("{id:int}")]
    //[ProducesResponseType(typeof(PersonModel), StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] EditiblePersonModel person)
    {
        var existingEntry = await _context.People.FindAsync(id);

        if (existingEntry is null)
            return NotFound();

        existingEntry.PreferedName = person.PreferedName;
        existingEntry.Emails = person.Emails;
        existingEntry.Pronouns = person.Pronouns;
        existingEntry.Socials = person.Socials;
        existingEntry.Addresses = person.Addresses;
        existingEntry.PhoneNumbers = person.PhoneNumbers;

        existingEntry.Name = person.Name;
        existingEntry.IsHidden = person.IsHidden;

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

