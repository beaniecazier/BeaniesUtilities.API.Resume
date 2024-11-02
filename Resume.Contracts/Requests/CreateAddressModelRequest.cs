using BeaniesUtilities.APIUtilities.Models.Requests;
using Swashbuckle.AspNetCore.Annotations;

namespace Gay.TCazier.Resume.Contracts.Requests;

public class CreateAddressModelRequest : CreateBaseModelRequest
{
    [SwaggerSchema(Description = "The house or builing number", Format = "7")]
    public required int HouseNumber { get; init; }

    [SwaggerSchema(Description = "The name of the road where an address is located.", Format = "7")]
    public required string StreetName { get; init; }

    [SwaggerSchema(Description = "", Format = "7")]
    public required string StreetType { get; init; }

    [SwaggerSchema(Description = "The term City means an inhabited place of greater size, population, or importance compared to smaller entities at communal level like urban districts.", Format = "7")]
    public required string City { get; init; }

    [SwaggerSchema(Description = "", Nullable = true, Format = "7")]
    public required string? Region { get; init; } = string.Empty;

    [SwaggerSchema(Description = "The State describes the first-level subdivision of a country (where applicable). This subdivision may have a different name in a given country, such as province, canton, territory, or department. The term State is used independently of this domestic terminology.", Format = "7")]
    public required string State { get; init; }

    [SwaggerSchema(Description = "Countries can be identified by name, ISO code or country code plate. The definition what belongs to a country in a map may depend on the data provider.", Format = "7")]
    public required string Country { get; init; }

    [SwaggerSchema(Description = "A postal code (or zip-code) is used by a country's postal authority to identify where an address is located.", Format = "7")]
    public required int PostalCode { get; init; }

    [SwaggerSchema(Description = "USA specific subdivision of the ZIP code", Nullable = true, Format = "7")]
    public required int? Zip4 { get; init; } = null;

    [SwaggerSchema(Description = "", Nullable = true, Format = "7")]
    public required string? CrossStreetName { get; init; } = string.Empty;

    [SwaggerSchema(Description = "", Nullable = true, Format = "7")]
    public required string? PrefixDirection { get; init; } = string.Empty;

    [SwaggerSchema(Description = "", Nullable = true, Format = "7")]
    public required string? PrefixType { get; init; } = string.Empty;

    [SwaggerSchema(Description = "", Nullable = true, Format = "7")]
    public required string? SuffixDirection { get; init; } = string.Empty;

    [SwaggerSchema(Description = "", Nullable = true, Format = "7")]
    public required string? SuffixType { get; init; } = string.Empty;
}
