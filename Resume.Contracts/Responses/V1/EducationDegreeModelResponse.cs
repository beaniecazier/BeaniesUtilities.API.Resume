using BeaniesUtilities.APIUtilities.Models.Responses;
using Swashbuckle.AspNetCore.Annotations;
using BeaniesUtilities.Models.Resume;
using BeaniesUtilities.Models.Enum;

namespace Gay.TCazier.Resume.Contracts.Responses.V1;

public class EducationDegreeModelResponse : BaseModelResponse
{
	[SwaggerSchema(Description = "hi", Format = "7")]
	public required float GPA { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required DateTime? StartDate { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required DateTime? EndDate { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required EducationInstitutionModel Institution { get; init; }

}