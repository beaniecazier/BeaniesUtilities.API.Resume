using BeaniesUtilities.APIUtilities.Models.Requests;
using Swashbuckle.AspNetCore.Annotations;
using BeaniesUtilities.Models.Resume;
using BeaniesUtilities.Models.Enum;

namespace Gay.TCazier.Resume.Contracts.Requests.V1;

public class UpdatePersonModelRequest : UpdateBaseModelRequest
{
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
