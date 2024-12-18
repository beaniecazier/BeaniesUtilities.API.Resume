using BeaniesUtilities.APIUtilities.Models.Requests;
using Swashbuckle.AspNetCore.Annotations;
using BeaniesUtilities.Models.Resume;
using BeaniesUtilities.Models.Enum;

namespace Gay.TCazier.Resume.Contracts.Requests.V1;

public class UpdateWorkExperienceModelRequest : UpdateBaseModelRequest
{
	[SwaggerSchema(Description = "hi", Format = "7")]
	public required DateTime? StartDate { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required DateTime? EndDate { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required string? Company { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required string? Description { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required List<string> Responsibilities { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required int[] TechUsed { get; init; }

}
