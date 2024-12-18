using BeaniesUtilities.APIUtilities.Models.Responses;
using Swashbuckle.AspNetCore.Annotations;
using BeaniesUtilities.Models.Resume;
using BeaniesUtilities.Models.Enum;

namespace Gay.TCazier.Resume.Contracts.Responses.V1;

public class PersonModelResponse : BaseModelResponse
{
	[SwaggerSchema(Description = "hi", Format = "7")]
	public required string PreferedName { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required List<ePronoun> Pronouns { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required List<string> Emails { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required List<string> Socials { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required List<AddressModel> Addresses { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required List<PhoneNumberModel> PhoneNumbers { get; init; }

}