using BeaniesUtilities.APIUtilities.Models.Responses;
using Swashbuckle.AspNetCore.Annotations;
using BeaniesUtilities.Models.Resume;
using BeaniesUtilities.Models.Enum;

namespace Gay.TCazier.Resume.Contracts.Responses.V1;

public class EducationInstitutionModelResponse : BaseModelResponse
{
	[SwaggerSchema(Description = "hi", Format = "7")]
	public required string Website { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required AddressModel Address { get; init; }

	//[SwaggerSchema(Description = "hi", Format = "7")]
	//public required List<CertificateModel> CertificatesIssued { get; init; }

	//[SwaggerSchema(Description = "hi", Format = "7")]
	//public required List<EducationDegreeModel> DegreesGiven { get; init; }

}