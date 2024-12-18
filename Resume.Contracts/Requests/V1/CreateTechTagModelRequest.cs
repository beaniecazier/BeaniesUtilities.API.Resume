using BeaniesUtilities.APIUtilities.Models.Requests;
using Swashbuckle.AspNetCore.Annotations;
using BeaniesUtilities.Models.Resume;
using BeaniesUtilities.Models.Enum;

namespace Gay.TCazier.Resume.Contracts.Requests.V1;

public class CreateTechTagModelRequest : CreateBaseModelRequest
{
	[SwaggerSchema(Description = "hi", Format = "7")]
	public required string URL { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required string Description { get; init; }

}