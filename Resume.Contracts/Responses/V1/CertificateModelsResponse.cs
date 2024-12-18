namespace Gay.TCazier.Resume.Contracts.Responses.V1;

public class CertificateModelsResponse
{
    public required IEnumerable<CertificateModelResponse> Items { get; init; } = Enumerable.Empty<CertificateModelResponse>();
}