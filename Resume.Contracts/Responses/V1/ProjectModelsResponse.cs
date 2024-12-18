namespace Gay.TCazier.Resume.Contracts.Responses.V1;

public class ProjectModelsResponse
{
    public required IEnumerable<ProjectModelResponse> Items { get; init; } = Enumerable.Empty<ProjectModelResponse>();
}