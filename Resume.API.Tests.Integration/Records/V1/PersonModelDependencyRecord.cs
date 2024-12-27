namespace Resume.API.Tests.Integration.Records.V1;

public record PersonModelDependencyRecord
{
	public required int[] Addresses { get; set; }
	public required int[] PhoneNumbers { get; set; }
}