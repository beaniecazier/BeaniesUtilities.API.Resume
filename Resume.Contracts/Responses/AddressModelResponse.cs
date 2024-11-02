using BeaniesUtilities.APIUtilities.Models.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace Gay.TCazier.Resume.Contracts.Responses;

public class AddressModelResponse : BaseModelResponse
{
    [SwaggerSchema(Description = "hi", Format = "7")]
    public required int HouseNumber { get; init; }

    [SwaggerSchema(Description = "", Format = "7")]
    public required string StreetName { get; init; }

    [SwaggerSchema(Description = "", Format = "7")]
    public required string StreetType { get; init; }

    [SwaggerSchema(Description = "", Format = "7")]
    public required string City { get; init; }

    [SwaggerSchema(Description = "", Nullable = true, Format = "7")]
    public string? Region { get; set; }

    [SwaggerSchema(Description = "", Format = "7")]
    public required string State { get; init; }

    [SwaggerSchema(Description = "", Format = "7")]
    public required string Country { get; init; }

    [SwaggerSchema(Description = "", Format = "7")]
    public required int PostalCode { get; init; }

    [SwaggerSchema(Description = "", Nullable = true, Format = "7")]
    public int? Zip4 { get; set; }

    [SwaggerSchema(Description = "", Nullable = true, Format = "7")]
    public string? CrossStreetName { get; set; }

    [SwaggerSchema(Description = "", Nullable = true, Format = "7")]
    public string? PrefixDirection { get; set; }

    [SwaggerSchema(Description = "", Nullable = true, Format = "7")]
    public string? PrefixType { get; set; }

    [SwaggerSchema(Description = "", Nullable = true, Format = "7")]
    public string? SuffixDirection { get; set; }

    [SwaggerSchema(Description = "", Nullable = true, Format = "7")]
    public string? SuffixType { get; set; }
}
