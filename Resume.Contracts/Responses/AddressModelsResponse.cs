namespace Gay.TCazier.Resume.Contracts.Responses;

public class AddressModelsResponse
{
    public required IEnumerable<AddressModelResponse> Items { get; init; } = Enumerable.Empty<AddressModelResponse>();
}
