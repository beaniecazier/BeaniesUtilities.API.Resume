using BeaniesUtilities.APIUtilities.Models.Responses;
using Swashbuckle.AspNetCore.Annotations;
using BeaniesUtilities.Models.Resume;
using BeaniesUtilities.Models.Enum;

namespace Gay.TCazier.Resume.Contracts.Responses.V1;

public class PhoneNumberModelResponse : BaseModelResponse
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