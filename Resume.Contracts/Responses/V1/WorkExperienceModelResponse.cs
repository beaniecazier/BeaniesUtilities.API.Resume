using BeaniesUtilities.APIUtilities.Models.Responses;
using Swashbuckle.AspNetCore.Annotations;
using BeaniesUtilities.Models.Resume;
using BeaniesUtilities.Models.Enum;

namespace Gay.TCazier.Resume.Contracts.Responses.V1;

public class WorkExperienceModelResponse : BaseModelResponse
{
	[SwaggerSchema(Description = "hi", Format = "7")]
	public required DateTime StartDate { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required DateTime EndDate { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required string Company { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required string Description { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required List<string> Responsibilities { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required List<TechTagModel> TechUsed { get; init; }

}