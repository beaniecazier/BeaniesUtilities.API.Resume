using BeaniesUtilities.APIUtilities.Models.Requests;
using Swashbuckle.AspNetCore.Annotations;
using BeaniesUtilities.Models.Resume;
using BeaniesUtilities.Models.Enum;

namespace Gay.TCazier.Resume.Contracts.Requests.V1;

public class UpdateResumeModelRequest : UpdateBaseModelRequest
{
	[SwaggerSchema(Description = "hi", Format = "7")]
	public required string? HeroStatement { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required int[] Degrees { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required int[] Certificates { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required int[] WorkExperience { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required int[] Projects { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required string? PreferedName { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required List<ePronoun> Pronouns { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required List<string> Emails { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required List<string> Socials { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required int[] Addresses { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required int[] PhoneNumbers { get; init; }

}
