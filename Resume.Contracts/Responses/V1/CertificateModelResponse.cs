using BeaniesUtilities.APIUtilities.Models.Responses;
using Swashbuckle.AspNetCore.Annotations;
using BeaniesUtilities.Models.Resume;
using BeaniesUtilities.Models.Enum;

namespace Gay.TCazier.Resume.Contracts.Responses.V1;

public class CertificateModelResponse : BaseModelResponse
{
	[SwaggerSchema(Description = "hi", Format = "7")]
	public required DateTime IssueDate { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required string Link { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required string PdfFileName { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required EducationInstitutionModel Issuer { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required string CertificateID { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required DateTime ExpirationDate { get; init; }

}