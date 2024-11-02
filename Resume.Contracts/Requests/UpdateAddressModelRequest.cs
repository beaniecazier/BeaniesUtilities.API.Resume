using BeaniesUtilities.APIUtilities.Models.Requests;
using Swashbuckle.AspNetCore.Annotations;

namespace Gay.TCazier.Resume.Contracts.Requests;

public class UpdateAddressModelRequest : UpdateBaseModelRequest
{
    [SwaggerSchema(Description = "hi", Format = "7")]
    public required int? HouseNumber { get; init; }

    [SwaggerSchema(Description = "", Format = "7")]
    public required string? StreetName { get; init; }

    [SwaggerSchema(Description = "", Format = "7")]
    public required string? StreetType { get; init; }

    [SwaggerSchema(Description = "", Format = "7")]
    public required string? City { get; init; }

    [SwaggerSchema(Description = "", Nullable = true, Format = "7")]
    public required string? Region { get; init; }

    [SwaggerSchema(Description = "", Format = "7")]
    public required string? State { get; init; }

    [SwaggerSchema(Description = "", Format = "7")]
    public required string? Country { get; init; }

    [SwaggerSchema(Description = "", Format = "7")]
    public required int? PostalCode { get; init; }

    [SwaggerSchema(Description = "", Nullable = true, Format = "7")]
    public required int? Zip4 { get; init; }

    [SwaggerSchema(Description = "", Nullable = true, Format = "7")]
    public required string? CrossStreetName { get; init; }

    [SwaggerSchema(Description = "", Nullable = true, Format = "7")]
    public string? PrefixDirection { get; init; }

    [SwaggerSchema(Description = "", Nullable = true, Format = "7")]
    public required string? PrefixType { get; init; }

    [SwaggerSchema(Description = "", Nullable = true, Format = "7")]
    public required string? SuffixDirection { get; init; }

    [SwaggerSchema(Description = "", Nullable = true, Format = "7")]
    public required string? SuffixType { get; init; }
}
