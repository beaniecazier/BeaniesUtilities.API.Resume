namespace Gay.TCazier.Resume.Contracts.Responses.V1;

public class TechTagModelsResponse
{
    public required IEnumerable<TechTagModelResponse> Items { get; init; } = Enumerable.Empty<TechTagModelResponse>();
}