using BeaniesUtilities.APIUtilities.Models.Requests;
using Swashbuckle.AspNetCore.Annotations;
using BeaniesUtilities.Models.Resume;
using BeaniesUtilities.Models.Enum;

namespace Gay.TCazier.Resume.Contracts.Requests.V1.Update;

public class UpdateEducationDegreeModelRequest : UpdateBaseModelRequest
{
    [SwaggerSchema(Description = "hi", Format = "7")]
    public required float? GPA { get; init; }

    [SwaggerSchema(Description = "hi", Format = "7")]
    public required DateTime? StartDate { get; init; }

    [SwaggerSchema(Description = "hi", Format = "7")]
    public required DateTime? EndDate { get; init; }

    [SwaggerSchema(Description = "hi", Format = "7")]
    public required int? Institution { get; init; }

}
