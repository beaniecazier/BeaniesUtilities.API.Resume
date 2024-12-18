using BeaniesUtilities.APIUtilities.Models.Requests;
using Swashbuckle.AspNetCore.Annotations;
using BeaniesUtilities.Models.Resume;
using BeaniesUtilities.Models.Enum;

namespace Gay.TCazier.Resume.Contracts.Requests.V1;

public class UpdateEducationInstitutionModelRequest : UpdateBaseModelRequest
{
	[SwaggerSchema(Description = "hi", Format = "7")]
	public required string? Website { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required int? Address { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required int[] CertificatesIssued { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required int[] DegreesGiven { get; init; }

}
