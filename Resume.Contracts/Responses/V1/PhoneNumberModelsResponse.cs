namespace Gay.TCazier.Resume.Contracts.Responses.V1;

public class PhoneNumberModelsResponse
{
    public required IEnumerable<PhoneNumberModelResponse> Items { get; init; } = Enumerable.Empty<PhoneNumberModelResponse>();
}