using Gay.TCazier.Resume.Contracts.Requests.V1.GetAll;
using LanguageExt;

namespace Resume.API.Tests.Integration;

public static class GetAllPagedRequestExtensions
{
    public static string ToSearchTermsString(this GetAllAddressModelsRequest request)
    {
        List<string> results = new List<string>();
        if (request.AllowDeleted.HasValue) results.Add($"AllowDeleted={request.AllowDeleted.Value}");
        if (request.AllowHidden.HasValue) results.Add($"AllowHidden={request.AllowHidden.Value}");
        if (string.IsNullOrWhiteSpace(request.NameSearchTerm)) results.Add($"NameSearchTerm={request.NameSearchTerm}");
        if (string.IsNullOrWhiteSpace(request.NotesSearchTerm)) results.Add($"NotesSearchTerm={request.NotesSearchTerm}");
        if (request.AfterDate.HasValue) results.Add($"AfterDate={request.AfterDate.Value}");
        if (request.BeforeDate.HasValue) results.Add($"BeforeDate={request.BeforeDate.Value}");
        if (request.GreaterThanOrEqualToID.HasValue) results.Add($"GreaterThanOrEqualToID={request.GreaterThanOrEqualToID.Value}");
        if (request.LessThanOrEqualToID.HasValue) results.Add($"LessThanOrEqualToID={request.LessThanOrEqualToID.Value}");
        if (request.SpecificIds.Any()) results.Add(string.Join("&", request.SpecificIds.Select(x => $"SpecificIds={x}")));
        results.Add($"PageIndex={request.PageIndex}");
        results.Add($"PageSize={request.PageSize}");
        return string.Join("&", results);
    }
    public static string ToSearchTermsString(this GetAllCertificateModelsRequest request)
    {
        List<string> results = new List<string>();
        if (request.AllowDeleted.HasValue) results.Add($"AllowDeleted={request.AllowDeleted.Value}");
        if (request.AllowHidden.HasValue) results.Add($"AllowHidden={request.AllowHidden.Value}");
        if (string.IsNullOrWhiteSpace(request.NameSearchTerm)) results.Add($"NameSearchTerm={request.NameSearchTerm}");
        if (string.IsNullOrWhiteSpace(request.NotesSearchTerm)) results.Add($"NotesSearchTerm={request.NotesSearchTerm}");
        if (request.AfterDate.HasValue) results.Add($"AfterDate={request.AfterDate.Value}");
        if (request.BeforeDate.HasValue) results.Add($"BeforeDate={request.BeforeDate.Value}");
        if (request.GreaterThanOrEqualToID.HasValue) results.Add($"GreaterThanOrEqualToID={request.GreaterThanOrEqualToID.Value}");
        if (request.LessThanOrEqualToID.HasValue) results.Add($"LessThanOrEqualToID={request.LessThanOrEqualToID.Value}");
        if (request.SpecificIds.Any()) results.Add(string.Join("&", request.SpecificIds.Select(x => $"SpecificIds={x}")));
        results.Add($"PageIndex={request.PageIndex}");
        results.Add($"PageSize={request.PageSize}");
        return string.Join("&", results);
    }
    public static string ToSearchTermsString(this GetAllEducationDegreeModelsRequest request)
    {
        List<string> results = new List<string>();
        if (request.AllowDeleted.HasValue) results.Add($"AllowDeleted={request.AllowDeleted.Value}");
        if (request.AllowHidden.HasValue) results.Add($"AllowHidden={request.AllowHidden.Value}");
        if (string.IsNullOrWhiteSpace(request.NameSearchTerm)) results.Add($"NameSearchTerm={request.NameSearchTerm}");
        if (string.IsNullOrWhiteSpace(request.NotesSearchTerm)) results.Add($"NotesSearchTerm={request.NotesSearchTerm}");
        if (request.AfterDate.HasValue) results.Add($"AfterDate={request.AfterDate.Value}");
        if (request.BeforeDate.HasValue) results.Add($"BeforeDate={request.BeforeDate.Value}");
        if (request.GreaterThanOrEqualToID.HasValue) results.Add($"GreaterThanOrEqualToID={request.GreaterThanOrEqualToID.Value}");
        if (request.LessThanOrEqualToID.HasValue) results.Add($"LessThanOrEqualToID={request.LessThanOrEqualToID.Value}");
        if (request.SpecificIds.Any()) results.Add(string.Join("&", request.SpecificIds.Select(x => $"SpecificIds={x}")));
        results.Add($"PageIndex={request.PageIndex}");
        results.Add($"PageSize={request.PageSize}");
        return string.Join("&", results);
    }
    public static string ToSearchTermsString(this GetAllEducationInstitutionModelsRequest request)
    {
        List<string> results = new List<string>();
        if (request.AllowDeleted.HasValue) results.Add($"AllowDeleted={request.AllowDeleted.Value}");
        if (request.AllowHidden.HasValue) results.Add($"AllowHidden={request.AllowHidden.Value}");
        if (string.IsNullOrWhiteSpace(request.NameSearchTerm)) results.Add($"NameSearchTerm={request.NameSearchTerm}");
        if (string.IsNullOrWhiteSpace(request.NotesSearchTerm)) results.Add($"NotesSearchTerm={request.NotesSearchTerm}");
        if (request.AfterDate.HasValue) results.Add($"AfterDate={request.AfterDate.Value}");
        if (request.BeforeDate.HasValue) results.Add($"BeforeDate={request.BeforeDate.Value}");
        if (request.GreaterThanOrEqualToID.HasValue) results.Add($"GreaterThanOrEqualToID={request.GreaterThanOrEqualToID.Value}");
        if (request.LessThanOrEqualToID.HasValue) results.Add($"LessThanOrEqualToID={request.LessThanOrEqualToID.Value}");
        if (request.SpecificIds.Any()) results.Add(string.Join("&", request.SpecificIds.Select(x => $"SpecificIds={x}")));
        results.Add($"PageIndex={request.PageIndex}");
        results.Add($"PageSize={request.PageSize}");
        return string.Join("&", results);
    }
    public static string ToSearchTermsString(this GetAllPersonModelsRequest request)
    {
        List<string> results = new List<string>();
        if (request.AllowDeleted.HasValue) results.Add($"AllowDeleted={request.AllowDeleted.Value}");
        if (request.AllowHidden.HasValue) results.Add($"AllowHidden={request.AllowHidden.Value}");
        if (string.IsNullOrWhiteSpace(request.NameSearchTerm)) results.Add($"NameSearchTerm={request.NameSearchTerm}");
        if (string.IsNullOrWhiteSpace(request.NotesSearchTerm)) results.Add($"NotesSearchTerm={request.NotesSearchTerm}");
        if (request.AfterDate.HasValue) results.Add($"AfterDate={request.AfterDate.Value}");
        if (request.BeforeDate.HasValue) results.Add($"BeforeDate={request.BeforeDate.Value}");
        if (request.GreaterThanOrEqualToID.HasValue) results.Add($"GreaterThanOrEqualToID={request.GreaterThanOrEqualToID.Value}");
        if (request.LessThanOrEqualToID.HasValue) results.Add($"LessThanOrEqualToID={request.LessThanOrEqualToID.Value}");
        if (request.SpecificIds.Any()) results.Add(string.Join("&", request.SpecificIds.Select(x => $"SpecificIds={x}")));
        results.Add($"PageIndex={request.PageIndex}");
        results.Add($"PageSize={request.PageSize}");
        return string.Join("&", results);
    }
    public static string ToSearchTermsString(this GetAllPhoneNumberModelsRequest request)
    {
        List<string> results = new List<string>();
        if (request.AllowDeleted.HasValue) results.Add($"AllowDeleted={request.AllowDeleted.Value}");
        if (request.AllowHidden.HasValue) results.Add($"AllowHidden={request.AllowHidden.Value}");
        if (string.IsNullOrWhiteSpace(request.NameSearchTerm)) results.Add($"NameSearchTerm={request.NameSearchTerm}");
        if (string.IsNullOrWhiteSpace(request.NotesSearchTerm)) results.Add($"NotesSearchTerm={request.NotesSearchTerm}");
        if (request.AfterDate.HasValue) results.Add($"AfterDate={request.AfterDate.Value}");
        if (request.BeforeDate.HasValue) results.Add($"BeforeDate={request.BeforeDate.Value}");
        if (request.GreaterThanOrEqualToID.HasValue) results.Add($"GreaterThanOrEqualToID={request.GreaterThanOrEqualToID.Value}");
        if (request.LessThanOrEqualToID.HasValue) results.Add($"LessThanOrEqualToID={request.LessThanOrEqualToID.Value}");
        if (request.SpecificIds.Any()) results.Add(string.Join("&", request.SpecificIds.Select(x => $"SpecificIds={x}")));
        results.Add($"PageIndex={request.PageIndex}");
        results.Add($"PageSize={request.PageSize}");
        return string.Join("&", results);
    }
    public static string ToSearchTermsString(this GetAllProjectModelsRequest request)
    {
        List<string> results = new List<string>();
        if (request.AllowDeleted.HasValue) results.Add($"AllowDeleted={request.AllowDeleted.Value}");
        if (request.AllowHidden.HasValue) results.Add($"AllowHidden={request.AllowHidden.Value}");
        if (string.IsNullOrWhiteSpace(request.NameSearchTerm)) results.Add($"NameSearchTerm={request.NameSearchTerm}");
        if (string.IsNullOrWhiteSpace(request.NotesSearchTerm)) results.Add($"NotesSearchTerm={request.NotesSearchTerm}");
        if (request.AfterDate.HasValue) results.Add($"AfterDate={request.AfterDate.Value}");
        if (request.BeforeDate.HasValue) results.Add($"BeforeDate={request.BeforeDate.Value}");
        if (request.GreaterThanOrEqualToID.HasValue) results.Add($"GreaterThanOrEqualToID={request.GreaterThanOrEqualToID.Value}");
        if (request.LessThanOrEqualToID.HasValue) results.Add($"LessThanOrEqualToID={request.LessThanOrEqualToID.Value}");
        if (request.SpecificIds.Any()) results.Add(string.Join("&", request.SpecificIds.Select(x => $"SpecificIds={x}")));
        results.Add($"PageIndex={request.PageIndex}");
        results.Add($"PageSize={request.PageSize}");
        return string.Join("&", results);
    }
    public static string ToSearchTermsString(this GetAllResumeModelsRequest request)
    {
        List<string> results = new List<string>();
        if (request.AllowDeleted.HasValue) results.Add($"AllowDeleted={request.AllowDeleted.Value}");
        if (request.AllowHidden.HasValue) results.Add($"AllowHidden={request.AllowHidden.Value}");
        if (string.IsNullOrWhiteSpace(request.NameSearchTerm)) results.Add($"NameSearchTerm={request.NameSearchTerm}");
        if (string.IsNullOrWhiteSpace(request.NotesSearchTerm)) results.Add($"NotesSearchTerm={request.NotesSearchTerm}");
        if (request.AfterDate.HasValue) results.Add($"AfterDate={request.AfterDate.Value}");
        if (request.BeforeDate.HasValue) results.Add($"BeforeDate={request.BeforeDate.Value}");
        if (request.GreaterThanOrEqualToID.HasValue) results.Add($"GreaterThanOrEqualToID={request.GreaterThanOrEqualToID.Value}");
        if (request.LessThanOrEqualToID.HasValue) results.Add($"LessThanOrEqualToID={request.LessThanOrEqualToID.Value}");
        if (request.SpecificIds.Any()) results.Add(string.Join("&", request.SpecificIds.Select(x => $"SpecificIds={x}")));
        results.Add($"PageIndex={request.PageIndex}");
        results.Add($"PageSize={request.PageSize}");
        return string.Join("&", results);
    }
    public static string ToSearchTermsString(this GetAllTechTagModelsRequest request)
    {
        List<string> results = new List<string>();
        if (request.AllowDeleted.HasValue) results.Add($"AllowDeleted={request.AllowDeleted.Value}");
        if (request.AllowHidden.HasValue) results.Add($"AllowHidden={request.AllowHidden.Value}");
        if (string.IsNullOrWhiteSpace(request.NameSearchTerm)) results.Add($"NameSearchTerm={request.NameSearchTerm}");
        if (string.IsNullOrWhiteSpace(request.NotesSearchTerm)) results.Add($"NotesSearchTerm={request.NotesSearchTerm}");
        if (request.AfterDate.HasValue) results.Add($"AfterDate={request.AfterDate.Value}");
        if (request.BeforeDate.HasValue) results.Add($"BeforeDate={request.BeforeDate.Value}");
        if (request.GreaterThanOrEqualToID.HasValue) results.Add($"GreaterThanOrEqualToID={request.GreaterThanOrEqualToID.Value}");
        if (request.LessThanOrEqualToID.HasValue) results.Add($"LessThanOrEqualToID={request.LessThanOrEqualToID.Value}");
        if (request.SpecificIds.Any()) results.Add(string.Join("&", request.SpecificIds.Select(x => $"SpecificIds={x}")));
        results.Add($"PageIndex={request.PageIndex}");
        results.Add($"PageSize={request.PageSize}");
        return string.Join("&", results);
    }
    public static string ToSearchTermsString(this GetAllWorkExperienceModelsRequest request)
    {
        List<string> results = new List<string>();
        if (request.AllowDeleted.HasValue) results.Add($"AllowDeleted={request.AllowDeleted.Value}");
        if (request.AllowHidden.HasValue) results.Add($"AllowHidden={request.AllowHidden.Value}");
        if (string.IsNullOrWhiteSpace(request.NameSearchTerm)) results.Add($"NameSearchTerm={request.NameSearchTerm}");
        if (string.IsNullOrWhiteSpace(request.NotesSearchTerm)) results.Add($"NotesSearchTerm={request.NotesSearchTerm}");
        if (request.AfterDate.HasValue) results.Add($"AfterDate={request.AfterDate.Value}");
        if (request.BeforeDate.HasValue) results.Add($"BeforeDate={request.BeforeDate.Value}");
        if (request.GreaterThanOrEqualToID.HasValue) results.Add($"GreaterThanOrEqualToID={request.GreaterThanOrEqualToID.Value}");
        if (request.LessThanOrEqualToID.HasValue) results.Add($"LessThanOrEqualToID={request.LessThanOrEqualToID.Value}");
        if (request.SpecificIds.Any()) results.Add(string.Join("&", request.SpecificIds.Select(x => $"SpecificIds={x}")));
        results.Add($"PageIndex={request.PageIndex}");
        results.Add($"PageSize={request.PageSize}");
        return string.Join("&", results);
    }
}
