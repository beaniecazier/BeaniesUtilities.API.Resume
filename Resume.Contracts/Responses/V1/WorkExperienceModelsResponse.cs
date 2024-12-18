namespace Gay.TCazier.Resume.Contracts.Responses.V1;

public class WorkExperienceModelsResponse
{
    public required IEnumerable<WorkExperienceModelResponse> Items { get; init; } = Enumerable.Empty<WorkExperienceModelResponse>();
}