using BeaniesUtilities.APIUtilities.Models.Requests;
using Swashbuckle.AspNetCore.Annotations;
using BeaniesUtilities.Models.Resume;
using BeaniesUtilities.Models.Enum;

namespace Gay.TCazier.Resume.Contracts.Requests.V1.Update;

public class UpdateTechTagModelRequest : UpdateBaseModelRequest
{
    [SwaggerSchema(Description = "hi", Format = "7")]
    public required string? URL { get; init; }

    [SwaggerSchema(Description = "hi", Format = "7")]
    public required string? Description { get; init; }

}
