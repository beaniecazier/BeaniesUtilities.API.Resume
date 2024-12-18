using BeaniesUtilities.APIUtilities.Models.Requests;
using Swashbuckle.AspNetCore.Annotations;
using BeaniesUtilities.Models.Resume;
using BeaniesUtilities.Models.Enum;

namespace Gay.TCazier.Resume.Contracts.Requests.V1;

public class CreateCertificateModelRequest : CreateBaseModelRequest
{
	[SwaggerSchema(Description = "hi", Format = "7")]
	public required DateTime IssueDate { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required string Link { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required string PdfFileName { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required int Issuer { get; init; }

}