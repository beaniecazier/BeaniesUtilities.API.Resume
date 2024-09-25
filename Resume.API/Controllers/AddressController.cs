using BeaniesUtilities.Models.Resume;
using Gay.TCazier.DatabaseParser.Data.Contexts;
using Gay.TCazier.DatabaseParser.Models.EditibleAttributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gay.TCazier.DatabaseParser.Controllers;

public class AddressController : Controller
{
    private readonly ResumeContext ctx;

    public AddressController(ResumeContext context)
    {
        ctx = context;
    }



    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(AddressModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var experience = await ctx.Addresses
            .SingleOrDefaultAsync(x => x.EntryIdentity == id);

        return experience == null ? NotFound() : Ok(experience);
    }

    //[HttpPost]
    //[ProducesResponseType(typeof(AddressModel), StatusCodes.Status201Created)]
    //public async Task<IActionResult> Create([FromBody] EditibleAddressModel editibleAttributes)
    //{

    //    return CreatedAtAction(nameof(Get), new { id = model.EntryIdentity }, model);
    //}

    //[HttpPut("{id:int}")]
    //[ProducesResponseType(typeof(AddressModel), StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> Update([FromRoute] int id, [FromBody] EditibleAddressModel address)
    //{
    //}

    //[HttpDelete("{id:int}")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> Remove([FromRoute] int id)
    //{
    //}
}