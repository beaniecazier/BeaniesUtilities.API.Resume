using BeaniesUtilities.APIUtilities.Models.Requests;
using Swashbuckle.AspNetCore.Annotations;
using BeaniesUtilities.Models.Resume;
using BeaniesUtilities.Models.Enum;

namespace Gay.TCazier.Resume.Contracts.Requests.V1;

public class CreatePhoneNumberModelRequest : CreateBaseModelRequest
{
	[SwaggerSchema(Description = "hi", Format = "7")]
	public required int CountryCode { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required int AreaCode { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required int TelephonePrefix { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required int LineNumber { get; init; }

}