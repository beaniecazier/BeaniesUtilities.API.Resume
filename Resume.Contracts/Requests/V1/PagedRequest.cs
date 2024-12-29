namespace Gay.TCazier.Resume.Contracts.Requests.V1;

public interface PagedRequest
{
    public int PageIndex { get; init; }
    public int PageSize { get; init; }
}