namespace Resume.API.Tests.Integration.Records.V1;

public record ResumeModelDependencyRecord
{
	public required int[] Degrees { get; set; }
	public required int[] Certificates { get; set; }
	public required int[] WorkExperience { get; set; }
	public required int[] Projects { get; set; }
	public required int[] Addresses { get; set; }
	public required int[] PhoneNumbers { get; set; }
}