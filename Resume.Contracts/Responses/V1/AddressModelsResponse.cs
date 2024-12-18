namespace Gay.TCazier.Resume.Contracts.Responses.V1;

public class AddressModelsResponse
{
    public required IEnumerable<AddressModelResponse> Items { get; init; } = Enumerable.Empty<AddressModelResponse>();
}