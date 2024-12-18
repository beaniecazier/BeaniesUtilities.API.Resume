using BeaniesUtilities.APIUtilities.Models.Requests;
using Swashbuckle.AspNetCore.Annotations;
using BeaniesUtilities.Models.Resume;
using BeaniesUtilities.Models.Enum;

namespace Gay.TCazier.Resume.Contracts.Requests.V1;

public class UpdateProjectModelRequest : UpdateBaseModelRequest
{
	[SwaggerSchema(Description = "hi", Format = "7")]
	public required string? Description { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required string? Version { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required string? ProjectUrl { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required DateTime? StartDate { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required DateTime? CompletionDate { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required int[] TechTags { get; init; }

}
