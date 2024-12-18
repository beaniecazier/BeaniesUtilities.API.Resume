using BeaniesUtilities.APIUtilities.Models.Responses;
using Swashbuckle.AspNetCore.Annotations;
using BeaniesUtilities.Models.Resume;
using BeaniesUtilities.Models.Enum;

namespace Gay.TCazier.Resume.Contracts.Responses.V1;

public class TechTagModelResponse : BaseModelResponse
{
	[SwaggerSchema(Description = "hi", Format = "7")]
	public required string URL { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required string Description { get; init; }

}