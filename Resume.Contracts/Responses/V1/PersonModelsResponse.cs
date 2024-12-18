namespace Gay.TCazier.Resume.Contracts.Responses.V1;

public class PersonModelsResponse
{
    public required IEnumerable<PersonModelResponse> Items { get; init; } = Enumerable.Empty<PersonModelResponse>();
}