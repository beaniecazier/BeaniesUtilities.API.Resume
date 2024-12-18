namespace Gay.TCazier.Resume.Contracts.Responses.V1;

public class EducationInstitutionModelsResponse
{
    public required IEnumerable<EducationInstitutionModelResponse> Items { get; init; } = Enumerable.Empty<EducationInstitutionModelResponse>();
}