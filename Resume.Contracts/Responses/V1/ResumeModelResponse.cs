using BeaniesUtilities.APIUtilities.Models.Responses;
using Swashbuckle.AspNetCore.Annotations;
using BeaniesUtilities.Models.Resume;
using BeaniesUtilities.Models.Enum;

namespace Gay.TCazier.Resume.Contracts.Responses.V1;

public class ResumeModelResponse : BaseModelResponse
{
	[SwaggerSchema(Description = "hi", Format = "7")]
	public required string HeroStatement { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required List<EducationDegreeModel> Degrees { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required List<CertificateModel> Certificates { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required List<WorkExperienceModel> WorkExperience { get; init; }

	[SwaggerSchema(Description = "hi", Format = "7")]
	public required List<ProjectModel> Projects { get; init; }

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