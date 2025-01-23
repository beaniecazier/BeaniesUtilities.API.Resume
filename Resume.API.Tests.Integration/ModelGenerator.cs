using BeaniesUtilities.Models.Resume;
using Bogus;
using Bogus.DataSets;
using Gay.TCazier.Resume.API.Endpoints.V1.Create;
using Gay.TCazier.Resume.Contracts.Endpoints.V1;
using Gay.TCazier.Resume.Contracts.Requests.V1;
using Gay.TCazier.Resume.Contracts.Requests.V1.Create;
using Gay.TCazier.Resume.Contracts.Requests.V1.GetAll;
using Gay.TCazier.Resume.Contracts.Requests.V1.Update;
using Gay.TCazier.Resume.Contracts.Responses.V1;
using Resume.API.Tests.Integration.Records.V1;
using System.Net.Http;

namespace Resume.API.Tests.Integration;

public class ModelGenerator
{
    #region Create Request Generators

    public static CreateAddressModelRequest GenerateNewCreateAddressModelRequest(AddressModelDependencyRecord modelRecord)
    {
        var faker = new Faker();
        var address = faker.Address.CardinalDirection();

        return new CreateAddressModelRequest
        {
            Name = "Test Address Model",
            Notes = "Model created for test purposes only",

            HouseNumber = ModelPropertyGenerator.CreateValidInt32Value(min: 100, max: 9999),
            StreetName = faker.Address.StreetName(),
            StreetType = faker.Address.StreetSuffix(),
            City = faker.Address.City(),
            Region = faker.Address.County(),
            State = faker.Address.State(),
            Country = faker.Address.Country(),
            PostalCode = ModelPropertyGenerator.CreateValidInt32Value(min: 10000, max: 99999),
            Zip4 = ModelPropertyGenerator.CreateValidInt32Value(min: 0, max: 9999),
            CrossStreetName = faker.Address.StreetName(),
            PrefixDirection = faker.Address.CardinalDirection(),
            PrefixType = faker.Address.StreetSuffix(),
            SuffixDirection = faker.Address.CardinalDirection(),
            SuffixType = faker.Address.StreetSuffix(),
        };
    }

    public static CreateCertificateModelRequest GenerateNewCreateCertificateModelRequest(CertificateModelDependencyRecord modelRecord)
    {
        return new CreateCertificateModelRequest
        {
            Name = "Test Certificate Model",
            Notes = "Model created for test purposes only",

            IssueDate = ModelPropertyGenerator.CreateValidDateTime(),
            Link = ModelPropertyGenerator.CreateValidHyperlink(),
            PdfFileName = ModelPropertyGenerator.CreateValidFileName(),
            Issuer = modelRecord.Issuer,
            CertificateID = ModelPropertyGenerator.CreateValidString(),
            ExpirationDate = ModelPropertyGenerator.CreateValidDateTime(),
        };
    }

    public static CreateEducationDegreeModelRequest GenerateNewCreateEducationDegreeModelRequest(EducationDegreeModelDependencyRecord modelRecord)
    {
        return new CreateEducationDegreeModelRequest
        {
            Name = "Test EducationDegree Model",
            Notes = "Model created for test purposes only",

            GPA = ModelPropertyGenerator.CreateValidFloatValue(min:0.0f, max:5.0f),
            StartDate = ModelPropertyGenerator.CreateValidDateTime(),
            EndDate = ModelPropertyGenerator.CreateValidDateTime(),
            Institution = modelRecord.Institution,
        };
    }

    public static CreateEducationInstitutionModelRequest GenerateNewCreateEducationInstitutionModelRequest(EducationInstitutionModelDependencyRecord modelRecord)
    {
        return new CreateEducationInstitutionModelRequest
        {
            Name = "Test EducationInstitution Model",
            Notes = "Model created for test purposes only",

            Website = ModelPropertyGenerator.CreateValidHyperlink(),
            Address = modelRecord.Address,
            //CertificatesIssued = certificateModelArr,
            //DegreesGiven = educationDegreeModelArr,
        };
    }

    public static CreatePersonModelRequest GenerateNewCreatePersonModelRequest(PersonModelDependencyRecord modelRecord)
    {
        return new CreatePersonModelRequest
        {
            Name = "Test Person Model",
            Notes = "Model created for test purposes only",

            PreferedName = ModelPropertyGenerator.CreateValidName(),
            Pronouns = new List<BeaniesUtilities.Models.Enum.ePronoun>() { BeaniesUtilities.Models.Enum.ePronoun.SheHer },
            Emails = ModelPropertyGenerator.CreateValidListOfEmails(),
            Socials = ModelPropertyGenerator.CreateValidListOfHyperlinks(),
            Addresses = modelRecord.Addresses,
            PhoneNumbers = modelRecord.PhoneNumbers,
        };
    }

    public static CreatePhoneNumberModelRequest GenerateNewCreatePhoneNumberModelRequest(PhoneNumberModelDependencyRecord modelRecord)
    {
        return new CreatePhoneNumberModelRequest
        {
            Name = "Test PhoneNumber Model",
            Notes = "Model created for test purposes only",

            CountryCode = ModelPropertyGenerator.CreateValidInt32Value(),
            AreaCode = ModelPropertyGenerator.CreateValidInt32Value(),
            TelephonePrefix = ModelPropertyGenerator.CreateValidInt32Value(),
            LineNumber = ModelPropertyGenerator.CreateValidInt32Value(),
        };
    }

    public static CreateProjectModelRequest GenerateNewCreateProjectModelRequest(ProjectModelDependencyRecord modelRecord)
    {
        return new CreateProjectModelRequest
        {
            Name = "Test Project Model",
            Notes = "Model created for test purposes only",

            Description = ModelPropertyGenerator.CreateValidString(),
            Version = ModelPropertyGenerator.CreateValidSemver(),
            ProjectUrl = ModelPropertyGenerator.CreateValidHyperlink(),
            StartDate = ModelPropertyGenerator.CreateValidDateTime(),
            CompletionDate = ModelPropertyGenerator.CreateValidDateTime(),
            TechTags = modelRecord.TechTags,
        };
    }

    public static CreateResumeModelRequest GenerateNewCreateResumeModelRequest(ResumeModelDependencyRecord modelRecord)
    {
        return new CreateResumeModelRequest
        {
            Name = "Test Resume Model",
            Notes = "Model created for test purposes only",

            HeroStatement = ModelPropertyGenerator.CreateValidString(),
            Degrees = modelRecord.Degrees,
            Certificates = modelRecord.Certificates,
            WorkExperience = modelRecord.WorkExperience,
            Projects = modelRecord.Projects,
            PreferedName = ModelPropertyGenerator.CreateValidName(),
            Pronouns = new List<BeaniesUtilities.Models.Enum.ePronoun>() { BeaniesUtilities.Models.Enum.ePronoun.SheHer },
            Emails = ModelPropertyGenerator.CreateValidListOfEmails(),
            Socials = ModelPropertyGenerator.CreateValidListOfHyperlinks(),
            Addresses = modelRecord.Addresses,
            PhoneNumbers = modelRecord.PhoneNumbers,
        };
    }

    public static CreateTechTagModelRequest GenerateNewCreateTechTagModelRequest(TechTagModelDependencyRecord modelRecord)
    {
        return new CreateTechTagModelRequest
        {
            Name = "Test TechTag Model",
            Notes = "Model created for test purposes only",

            URL = ModelPropertyGenerator.CreateValidHyperlink(),
            Description = ModelPropertyGenerator.CreateValidString(),
        };
    }

    public static CreateWorkExperienceModelRequest GenerateNewCreateWorkExperienceModelRequest(WorkExperienceModelDependencyRecord modelRecord)
    {
        return new CreateWorkExperienceModelRequest
        {
            Name = "Test WorkExperience Model",
            Notes = "Model created for test purposes only",

            StartDate = ModelPropertyGenerator.CreateValidDateTime(),
            EndDate = ModelPropertyGenerator.CreateValidDateTime(),
            Company = ModelPropertyGenerator.CreateValidString(),
            Description = ModelPropertyGenerator.CreateValidString(),
            Responsibilities = ModelPropertyGenerator.CreateValidListOfStrings(),
            TechUsed = modelRecord.TechUsed,
        };
    }

    #endregion

    #region GetAll Request Generators

    public static GetAllAddressModelsRequest GenerateNewGetAllAddressModelRequest(int pageIndex, int pageSize,
        string name = "", string note = "",
        DateTime? after = null, DateTime? before = null,
        int? idLowerBound = null, int? idUpperBound = null,
        bool allowHidden = false, bool allowDeleted = false,
        int[] ids = null, string sortBy = "")
    {
        return new GetAllAddressModelsRequest()
        {
            NameSearchTerm = name,
            NotesSearchTerm = note,
            AfterDate = after,
            BeforeDate = before,
            GreaterThanOrEqualToID = idLowerBound,
            LessThanOrEqualToID = idUpperBound,
            AllowHidden = allowHidden,             //doe these need to be nullable or can they default to false
            AllowDeleted = allowDeleted,            //doe these need to be nullable or can they default to false
            SpecificIds = ids is null ? new int[] { } : ids,
            PageIndex = pageIndex,
            PageSize = pageSize,
            SortBy = sortBy,
        };
    }

    public static GetAllCertificateModelsRequest GenerateNewGetAllCertificateModelRequest(int pageIndex, int pageSize,
        string name = "", string note = "",
        DateTime? after = null, DateTime? before = null,
        int? idLowerBound = null, int? idUpperBound = null,
        bool allowHidden = false, bool allowDeleted = false,
        int[] ids = null, string sortBy = "")
    {
        return new GetAllCertificateModelsRequest()
        {
            NameSearchTerm = name,
            NotesSearchTerm = note,
            AfterDate = after,
            BeforeDate = before,
            GreaterThanOrEqualToID = idLowerBound,
            LessThanOrEqualToID = idUpperBound,
            AllowHidden = allowHidden,             //doe these need to be nullable or can they default to false
            AllowDeleted = allowDeleted,            //doe these need to be nullable or can they default to false
            SpecificIds = ids is null ? new int[] { } : ids,
            PageIndex = pageIndex,
            PageSize = pageSize,
            SortBy = sortBy,
        };
    }

    public static GetAllEducationDegreeModelsRequest GenerateNewGetAllEducationDegreeModelRequest(int pageIndex, int pageSize,
        string name = "", string note = "",
        DateTime? after = null, DateTime? before = null,
        int? idLowerBound = null, int? idUpperBound = null,
        bool allowHidden = false, bool allowDeleted = false,
        int[] ids = null, string sortBy = "")
    {
        return new GetAllEducationDegreeModelsRequest()
        {
            NameSearchTerm = name,
            NotesSearchTerm = note,
            AfterDate = after,
            BeforeDate = before,
            GreaterThanOrEqualToID = idLowerBound,
            LessThanOrEqualToID = idUpperBound,
            AllowHidden = allowHidden,             //doe these need to be nullable or can they default to false
            AllowDeleted = allowDeleted,            //doe these need to be nullable or can they default to false
            SpecificIds = ids is null ? new int[] { } : ids,
            PageIndex = pageIndex,
            PageSize = pageSize,
            SortBy = sortBy,
        };
    }

    public static GetAllEducationInstitutionModelsRequest GenerateNewGetAllEducationInstitutionModelRequest(int pageIndex, int pageSize,
        string name = "", string note = "",
        DateTime? after = null, DateTime? before = null,
        int? idLowerBound = null, int? idUpperBound = null,
        bool allowHidden = false, bool allowDeleted = false,
        int[] ids = null, string sortBy = "")
    {
        return new GetAllEducationInstitutionModelsRequest()
        {
            NameSearchTerm = name,
            NotesSearchTerm = note,
            AfterDate = after,
            BeforeDate = before,
            GreaterThanOrEqualToID = idLowerBound,
            LessThanOrEqualToID = idUpperBound,
            AllowHidden = allowHidden,             //doe these need to be nullable or can they default to false
            AllowDeleted = allowDeleted,            //doe these need to be nullable or can they default to false
            SpecificIds = ids is null ? new int[] { } : ids,
            PageIndex = pageIndex,
            PageSize = pageSize,
            SortBy = sortBy,
        };
    }

    public static GetAllPersonModelsRequest GenerateNewGetAllPersonModelRequest(int pageIndex, int pageSize,
        string name = "", string note = "",
        DateTime? after = null, DateTime? before = null,
        int? idLowerBound = null, int? idUpperBound = null,
        bool allowHidden = false, bool allowDeleted = false,
        int[] ids = null, string sortBy = "")
    {
        return new GetAllPersonModelsRequest()
        {
            NameSearchTerm = name,
            NotesSearchTerm = note,
            AfterDate = after,
            BeforeDate = before,
            GreaterThanOrEqualToID = idLowerBound,
            LessThanOrEqualToID = idUpperBound,
            AllowHidden = allowHidden,             //doe these need to be nullable or can they default to false
            AllowDeleted = allowDeleted,            //doe these need to be nullable or can they default to false
            SpecificIds = ids is null ? new int[] { } : ids,
            PageIndex = pageIndex,
            PageSize = pageSize,
            SortBy = sortBy,
        };
    }

    public static GetAllPhoneNumberModelsRequest GenerateNewGetAllPhoneNumberModelRequest(int pageIndex, int pageSize,
        string name = "", string note = "",
        DateTime? after = null, DateTime? before = null,
        int? idLowerBound = null, int? idUpperBound = null,
        bool allowHidden = false, bool allowDeleted = false,
        int[] ids = null, string sortBy = "")
    {
        return new GetAllPhoneNumberModelsRequest()
        {
            NameSearchTerm = name,
            NotesSearchTerm = note,
            AfterDate = after,
            BeforeDate = before,
            GreaterThanOrEqualToID = idLowerBound,
            LessThanOrEqualToID = idUpperBound,
            AllowHidden = allowHidden,             //doe these need to be nullable or can they default to false
            AllowDeleted = allowDeleted,            //doe these need to be nullable or can they default to false
            SpecificIds = ids is null ? new int[] { } : ids,
            PageIndex = pageIndex,
            PageSize = pageSize,
            SortBy = sortBy,
        };
    }

    public static GetAllProjectModelsRequest GenerateNewGetAllProjectModelRequest(int pageIndex, int pageSize,
        string name = "", string note = "",
        DateTime? after = null, DateTime? before = null,
        int? idLowerBound = null, int? idUpperBound = null,
        bool allowHidden = false, bool allowDeleted = false,
        int[] ids = null, string sortBy = "")
    {
        return new GetAllProjectModelsRequest()
        {
            NameSearchTerm = name,
            NotesSearchTerm = note,
            AfterDate = after,
            BeforeDate = before,
            GreaterThanOrEqualToID = idLowerBound,
            LessThanOrEqualToID = idUpperBound,
            AllowHidden = allowHidden,             //doe these need to be nullable or can they default to false
            AllowDeleted = allowDeleted,            //doe these need to be nullable or can they default to false
            SpecificIds = ids is null ? new int[] { } : ids,
            PageIndex = pageIndex,
            PageSize = pageSize,
            SortBy = sortBy,
        };
    }

    public static GetAllResumeModelsRequest GenerateNewGetAllResumeModelRequest(int pageIndex, int pageSize,
        string name = "", string note = "",
        DateTime? after = null, DateTime? before = null,
        int? idLowerBound = null, int? idUpperBound = null,
        bool allowHidden = false, bool allowDeleted = false,
        int[] ids = null, string sortBy = "")
    {
        return new GetAllResumeModelsRequest()
        {
            NameSearchTerm = name,
            NotesSearchTerm = note,
            AfterDate = after,
            BeforeDate = before,
            GreaterThanOrEqualToID = idLowerBound,
            LessThanOrEqualToID = idUpperBound,
            AllowHidden = allowHidden,             //doe these need to be nullable or can they default to false
            AllowDeleted = allowDeleted,            //doe these need to be nullable or can they default to false
            SpecificIds = ids is null ? new int[] { } : ids,
            PageIndex = pageIndex,
            PageSize = pageSize,
            SortBy = sortBy,
        };
    }

    public static GetAllTechTagModelsRequest GenerateNewGetAllTechTagModelRequest(int pageIndex, int pageSize,
        string name = "", string note = "",
        DateTime? after = null, DateTime? before = null,
        int? idLowerBound = null, int? idUpperBound = null,
        bool allowHidden = false, bool allowDeleted = false,
        int[] ids = null, string sortBy = "")
    {
        return new GetAllTechTagModelsRequest()
        {
            NameSearchTerm = name,
            NotesSearchTerm = note,
            AfterDate = after,
            BeforeDate = before,
            GreaterThanOrEqualToID = idLowerBound,
            LessThanOrEqualToID = idUpperBound,
            AllowHidden = allowHidden,             //doe these need to be nullable or can they default to false
            AllowDeleted = allowDeleted,            //doe these need to be nullable or can they default to false
            SpecificIds = ids is null ? new int[] { } : ids,
            PageIndex = pageIndex,
            PageSize = pageSize,
            SortBy = sortBy,
        };
    }

    public static GetAllWorkExperienceModelsRequest GenerateNewGetAllWorkExperienceModelRequest(int pageIndex, int pageSize,
        string name = "", string note = "",
        DateTime? after = null, DateTime? before = null,
        int? idLowerBound = null, int? idUpperBound = null,
        bool allowHidden = false, bool allowDeleted = false,
        int[] ids = null, string sortBy = "")
    {
        return new GetAllWorkExperienceModelsRequest()
        {
            NameSearchTerm = name,
            NotesSearchTerm = note,
            AfterDate = after,
            BeforeDate = before,
            GreaterThanOrEqualToID = idLowerBound,
            LessThanOrEqualToID = idUpperBound,
            AllowHidden = allowHidden,             //doe these need to be nullable or can they default to false
            AllowDeleted = allowDeleted,            //doe these need to be nullable or can they default to false
            SpecificIds = ids is null ? new int[] { } : ids,
            PageIndex = pageIndex,
            PageSize = pageSize,
            SortBy = sortBy,
        };
    }

    #endregion

    public static async Task<AddressModelDependencyRecord> PopulateDatabaseForAddressModelTest(HttpClient client)
    {
        return new AddressModelDependencyRecord();
    }

    public static async Task<CertificateModelDependencyRecord> PopulateDatabaseForCertificateModelTest(HttpClient client)
    {
        var issuerRecord = await PopulateDatabaseForEducationInstitutionModelTest(client);
        var issuerRequest = GenerateNewCreateEducationInstitutionModelRequest(issuerRecord);
        var issuerResult = await client.PostAsJsonAsync(EducationInstitutionModelEndpoints.Post, issuerRequest);
        var issuerGet = await client.GetAsync(issuerResult.Headers.Location.AbsolutePath);
        var issuerResponse = await issuerGet.Content.ReadFromJsonAsync<EducationInstitutionModelResponse>();
        var issuerId = issuerResponse.Id;

        return new CertificateModelDependencyRecord()
        {
            Issuer = issuerId,
        };
    }

    public static async Task<EducationDegreeModelDependencyRecord> PopulateDatabaseForEducationDegreeModelTest(HttpClient client)
    {
        var instituteRecord = await PopulateDatabaseForEducationInstitutionModelTest(client);
        var institutionRequest = GenerateNewCreateEducationInstitutionModelRequest(instituteRecord);
        var institutionResult = await client.PostAsJsonAsync(EducationInstitutionModelEndpoints.Post, institutionRequest);
        var institutionGet = await client.GetAsync(institutionResult.Headers.Location.AbsolutePath);
        var institutionResponse = await institutionGet.Content.ReadFromJsonAsync<EducationInstitutionModelResponse>();
        var institutionId = institutionResponse.Id;

        return new EducationDegreeModelDependencyRecord()
        {
            Institution = institutionId,
        };
    }

    public static async Task<EducationInstitutionModelDependencyRecord> PopulateDatabaseForEducationInstitutionModelTest(HttpClient client)
    {
        var addressRecord = await PopulateDatabaseForAddressModelTest(client);
        var addressRequest = GenerateNewCreateAddressModelRequest(addressRecord);
        var addressResult = await client.PostAsJsonAsync(AddressModelEndpoints.Post, addressRequest);
        var addressGet = await client.GetAsync(addressResult.Headers.Location.AbsolutePath);
        var addressResponse = await addressGet.Content.ReadFromJsonAsync<AddressModelResponse>();
        var addressId = addressResponse.Id;

        return new EducationInstitutionModelDependencyRecord()
        {
            Address = addressId,
        };
    }

    public static async Task<PersonModelDependencyRecord> PopulateDatabaseForPersonModelTest(HttpClient client)
    {
        var addressRecord = await PopulateDatabaseForAddressModelTest(client);
        var addressRequest = GenerateNewCreateAddressModelRequest(addressRecord);
        var addressResult = await client.PostAsJsonAsync(AddressModelEndpoints.Post, addressRequest);
        var addressGet = await client.GetAsync(addressResult.Headers.Location.AbsolutePath);
        var addressResponse = await addressGet.Content.ReadFromJsonAsync<AddressModelResponse>();
        var addressId = addressResponse.Id;
        var addressModelArr = new int[] { addressId };

        var phoneNumberRecord = await PopulateDatabaseForPhoneNumberModelTest(client);
        var phoneNumberModelRequest = GenerateNewCreatePhoneNumberModelRequest(phoneNumberRecord);
        var phoneNumberModelResult = await client.PostAsJsonAsync(PhoneNumberModelEndpoints.Post, phoneNumberModelRequest);
        var phoneNumberGet = await client.GetAsync(phoneNumberModelResult.Headers.Location.AbsolutePath);
        var phoneNumberResponse = await phoneNumberGet.Content.ReadFromJsonAsync<PhoneNumberModelResponse>();
        var phoneNumberModelId = phoneNumberResponse.Id;
        var phoneNumberModelArr = new int[] { phoneNumberModelId };

        return new PersonModelDependencyRecord()
        {
            Addresses = addressModelArr,
            PhoneNumbers = phoneNumberModelArr
        };
    }

    public static async Task<PhoneNumberModelDependencyRecord> PopulateDatabaseForPhoneNumberModelTest(HttpClient client)
    {
        return new PhoneNumberModelDependencyRecord()
        {
        };
    }

    public static async Task<ProjectModelDependencyRecord> PopulateDatabaseForProjectModelTest(HttpClient client)
    {
        var techTagRecord = await PopulateDatabaseForTechTagModelTest(client);
        var techTagModelRequest = GenerateNewCreateTechTagModelRequest(techTagRecord);
        var techTagModelResult = await client.PostAsJsonAsync(TechTagModelEndpoints.Post, techTagModelRequest);
        var techTagGet = await client.GetAsync(techTagModelResult.Headers.Location.AbsolutePath);
        var techTagResponse = await techTagGet.Content.ReadFromJsonAsync<TechTagModelResponse>();
        var techTagModelId = techTagResponse.Id;
        var techTagModelArr = new int[] { techTagModelId };

        return new ProjectModelDependencyRecord()
        {
            TechTags = techTagModelArr,
        };
    }

    public static async Task<ResumeModelDependencyRecord> PopulateDatabaseForResumeModelTest(HttpClient client)
    {
        var educationDegreeRecord = await PopulateDatabaseForEducationDegreeModelTest(client);
        var educationDegreeModelRequest = GenerateNewCreateEducationDegreeModelRequest(educationDegreeRecord);
        var educationDegreeModelResult = await client.PostAsJsonAsync(EducationDegreeModelEndpoints.Post, educationDegreeModelRequest);
        var degrgeeGet = await client.GetAsync(educationDegreeModelResult.Headers.Location.AbsolutePath);
        var degreeResponse = await degrgeeGet.Content.ReadFromJsonAsync<EducationDegreeModelResponse>();
        var educationDegreeModelId = degreeResponse.Id;
        var educationDegreeModelArr = new int[] { educationDegreeModelId };

        var certificateRecord = await PopulateDatabaseForCertificateModelTest(client);
        var certificateModelRequest = GenerateNewCreateCertificateModelRequest(certificateRecord);
        var certificateModelResult = await client.PostAsJsonAsync(CertificateModelEndpoints.Post, certificateModelRequest);
        var certificateGet = await client.GetAsync(certificateModelResult.Headers.Location.AbsolutePath);
        var certificateResponse = await certificateGet.Content.ReadFromJsonAsync<CertificateModelResponse>();
        var certificateModelId = certificateResponse.Id;
        var certificateModelArr = new int[] { certificateModelId };

        var workExperienceRecord = await PopulateDatabaseForWorkExperienceModelTest(client);
        var workExperienceModelRequest = GenerateNewCreateWorkExperienceModelRequest(workExperienceRecord);
        var workExperienceModelResult = await client.PostAsJsonAsync(WorkExperienceModelEndpoints.Post, workExperienceModelRequest);
        var workExperienceGet = await client.GetAsync(workExperienceModelResult.Headers.Location.AbsolutePath);
        var workExperienceResponse = await workExperienceGet.Content.ReadFromJsonAsync<WorkExperienceModelResponse>();
        var workExperienceModelId = workExperienceResponse.Id;
        var workExperienceModelArr = new int[] { workExperienceModelId };

        var projectRecord = await PopulateDatabaseForProjectModelTest(client);
        var projectModelRequest = GenerateNewCreateProjectModelRequest(projectRecord);
        var projectModelResult = await client.PostAsJsonAsync(ProjectModelEndpoints.Post, projectModelRequest);
        var projectGet = await client.GetAsync(projectModelResult.Headers.Location.AbsolutePath);
        var projectResponse = await projectGet.Content.ReadFromJsonAsync<ProjectModelResponse>();
        var projectModelId = projectResponse.Id;
        var projectModelArr = new int[] { projectModelId };

        var addressRecord = await PopulateDatabaseForAddressModelTest(client);
        var addressRequest = GenerateNewCreateAddressModelRequest(addressRecord);
        var addressResult = await client.PostAsJsonAsync(AddressModelEndpoints.Post, addressRequest);
        var addressGet = await client.GetAsync(addressResult.Headers.Location.AbsolutePath);
        var addressResponse = await addressGet.Content.ReadFromJsonAsync<AddressModelResponse>();
        var addressId = addressResponse.Id;
        var addressModelArr = new int[] { addressId };

        var phoneNumberRecord = await PopulateDatabaseForPhoneNumberModelTest(client);
        var phoneNumberModelRequest = GenerateNewCreatePhoneNumberModelRequest(phoneNumberRecord);
        var phoneNumberModelResult = await client.PostAsJsonAsync(PhoneNumberModelEndpoints.Post, phoneNumberModelRequest);
        var phoneNumberGet = await client.GetAsync(phoneNumberModelResult.Headers.Location.AbsolutePath);
        var phoneNumberResponse = await phoneNumberGet.Content.ReadFromJsonAsync<PhoneNumberModelResponse>();
        var phoneNumberModelId = phoneNumberResponse.Id;
        var phoneNumberModelArr = new int[] { phoneNumberModelId };

        return new ResumeModelDependencyRecord()
        {
            Degrees = educationDegreeModelArr,
            Certificates = certificateModelArr,
            WorkExperience = workExperienceModelArr,
            Projects = projectModelArr,
            Addresses = addressModelArr,
            PhoneNumbers = phoneNumberModelArr,
        };
    }

    public static async Task<TechTagModelDependencyRecord> PopulateDatabaseForTechTagModelTest(HttpClient client)
    {
        return new TechTagModelDependencyRecord()
        {
        };
    }

    public static async Task<WorkExperienceModelDependencyRecord> PopulateDatabaseForWorkExperienceModelTest(HttpClient client)
    {
        var techTagRecord = await PopulateDatabaseForTechTagModelTest(client);
        var techTagModelRequest = GenerateNewCreateTechTagModelRequest(techTagRecord);
        var techTagModelResult = await client.PostAsJsonAsync(TechTagModelEndpoints.Post, techTagModelRequest);
        var techTagGet = await client.GetAsync(techTagModelResult.Headers.Location.AbsolutePath);
        var techTagResponse = await techTagGet.Content.ReadFromJsonAsync<TechTagModelResponse>();
        var techTagModelId = techTagResponse.Id;
        var techTagModelArr = new int[] { techTagModelId };

        return new WorkExperienceModelDependencyRecord()
        {
            TechUsed = techTagModelArr,
        };
    }
}
