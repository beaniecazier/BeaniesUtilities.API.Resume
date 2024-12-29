using Swashbuckle.AspNetCore.Annotations;

namespace Gay.TCazier.Resume.Contracts.Requests.V1.GetAll;

public class GetAllProjectModelsRequest : PagedRequest
{
    /// <param name="nameSearchTerm">REGEX used to query for models with matching name field</param>
    public required string? NameSearchTerm { get; init; }

    /// <param name="notesSearchTerm">REGEX to query for models across all notes</param>
    public required string? NotesSearchTerm { get; init; }

    /// <param name="afterDate">Date and time for start range of query. If left empty, all newModel that were last modified or created before end will be returned</param>
    public required DateTime? AfterDate { get; init; }

    /// <param name="beforeDate">Date and time for the end of the search range. If left empty, all newModel that were last modified or created after start will be returned</param>
    public required DateTime? BeforeDate { get; init; }

    /// <param name="allowHidden">Allow query to include hidden entries</param>
    public required bool? AllowHidden { get; init; }

    /// <param name="allowDeleted">Allow query to include deleted entries</param>
    public required bool? AllowDeleted { get; init; }

    /// <param name="greaterThanOrEqualToID">Id search range lower bound. If left empty, all newModel that with an id before end will be returned</param>
    public required int? GreaterThanOrEqualToID { get; init; }

    /// <param name="lessThanOrEqualToID">Id search range upper bound. If left empty, all newModel that with an id before end will be returned</param>
    public required int? LessThanOrEqualToID { get; init; }

    /// <param name="specificIds">A collection of Ids to include in search</param>
    public required int[] SpecificIds { get; init; }

    public required int PageIndex { get; init; } = 0;
    public required int PageSize { get; init; } = 10;
}
