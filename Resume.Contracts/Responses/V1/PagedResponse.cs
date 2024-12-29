namespace Gay.TCazier.Resume.Contracts.Responses.V1;

public class PagedResponse<TResponse>
{
    public required IEnumerable<TResponse> Items { get; init; } = Enumerable.Empty<TResponse>();

    public required int PageSize { get; init; } = 10;
    public required int TotalNumberOfAvailableResponses { get; init; }
    public required int PageIndex { get; init; } = 1;
    public bool HasNexPage => TotalNumberOfAvailableResponses > PageIndex * PageSize;
}
