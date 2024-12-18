namespace Gay.TCazier.Resume.Contracts.Responses.V1;

public class EducationDegreeModelsResponse
{
    public required IEnumerable<EducationDegreeModelResponse> Items { get; init; } = Enumerable.Empty<EducationDegreeModelResponse>();
}