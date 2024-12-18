namespace Gay.TCazier.Resume.Contracts.Responses.V1;

public class ResumeModelsResponse
{
    public required IEnumerable<ResumeModelResponse> Items { get; init; } = Enumerable.Empty<ResumeModelResponse>();
}