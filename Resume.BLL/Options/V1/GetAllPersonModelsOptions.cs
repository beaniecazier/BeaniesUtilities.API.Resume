using Swashbuckle.AspNetCore.Annotations;

namespace Gay.TCazier.Resume.BLL.Options.V1;

public class GetAllPersonModelsOptions
{
    [SwaggerSchema(Description = "hi", Format = "7")]
    public int? ID { get; set; }

    public bool HasFilters => !string.IsNullOrWhiteSpace(NameSearchTerm) ||
                                !string.IsNullOrWhiteSpace(NotesSearchTerm) ||
                                AfterDate.HasValue ||
                                BeforeDate.HasValue ||
                                GreaterThanOrEqualToID.HasValue ||
                                LessThanOrEqualToID.HasValue ||
                                AllowDeleted.HasValue ||
                                AllowHidden.HasValue || 
                                SpecificIds.Count() > 0;

    [SwaggerSchema(Description = "hi", Format = "7")]
    public string? NameSearchTerm { get; set; }
    [SwaggerSchema(Description = "hi", Format = "7")]
    public string? NotesSearchTerm { get; set; }
    [SwaggerSchema(Description = "hi", Format = "7")]
    public DateTime? AfterDate { get; set; }
    [SwaggerSchema(Description = "hi", Format = "7")]
    public DateTime? BeforeDate { get; set; }
    [SwaggerSchema(Description = "hi", Format = "7")]
    public bool? AllowHidden { get; set; }
    [SwaggerSchema(Description = "hi", Format = "7")]
    public bool? AllowDeleted { get; set; }
    [SwaggerSchema(Description = "hi", Format = "7")]
    public int? GreaterThanOrEqualToID { get; set; }
    [SwaggerSchema(Description = "hi", Format = "7")]
    public int? LessThanOrEqualToID { get; set; }
    [SwaggerSchema(Description = "hi", Format = "7")]
    public required int[] SpecificIds { get; init; }
}
