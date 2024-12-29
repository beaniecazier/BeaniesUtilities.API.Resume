using BeaniesUtilities.Models.Resume;
using FluentAssertions;
using Gay.TCazier.Resume.API;
using Gay.TCazier.Resume.API.Endpoints.V1.Create;
using Gay.TCazier.Resume.API.Endpoints.V1.Delete;
using Gay.TCazier.Resume.API.Endpoints.V1.Get;
using Gay.TCazier.Resume.Contracts.Responses.V1;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace Resume.API.Tests.Integration.Endpoint.V1.Get;

public class PersonModelGetEndpointsTests : IClassFixture<WebApplicationFactory<IAPIMarker>>, IAsyncLifetime
{
    private readonly WebApplicationFactory<IAPIMarker> _factory;

    private List<int> _createdPersonModels = new List<int>();

    public PersonModelGetEndpointsTests(WebApplicationFactory<IAPIMarker> factory)
    {
        _factory = factory;
    }

    public async Task DisposeAsync()
    {
        var httpClient = _factory.CreateClient();
        foreach (int id in _createdPersonModels)
        {
            await httpClient.DeleteAsync($"{DeletePersonModelEndpoint.EndpointPrefix}/{id}");
        }
    }

    public Task InitializeAsync() => Task.CompletedTask;

    [Fact]
    public async Task GetPersonModel_ReturnsPersonModel_WhenModelExists()
    {
        // ARRANGE
        var httpClient = _factory.CreateClient();
        
        var modelRecord = await ModelGenerator.PopulateDatabaseForPersonModelTest(httpClient);
        var modelRequest = ModelGenerator.GenerateNewCreatePersonModelRequest(modelRecord);

        var create = await httpClient.PostAsJsonAsync(CreatePersonModelEndpoint.EndpointPrefix, modelRequest);
        var get = await httpClient.GetAsync(create.Headers.Location.AbsolutePath);
        var createdModel = await get.Content.ReadFromJsonAsync<PersonModel>();
        _createdPersonModels.Add(createdModel.CommonIdentity);

        // ACT
        var result = await httpClient.GetAsync($"{GetPersonModelEndpoint.EndpointPrefix}/{createdModel.CommonIdentity}");
        var foundModel = await result.Content.ReadFromJsonAsync<PersonModel>();

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        foundModel.Should().BeEquivalentTo(createdModel);
    }

    [Fact]
    public async Task GetPersonModel_ReturnsNotFound_WhenModelDoesNotExist()
    {
        // ARRANGE
        var httpClient = _factory.CreateClient();

        // ACT
        var result = await httpClient.GetAsync($"{GetPersonModelEndpoint.EndpointPrefix}/{-1000000}");

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetAllPersonModels_ReturnsAllModelsUnsorted_WhenModelsExist()
    {
        int numberOfModelsToMake = 5;
        int pageNumberForTest = 0;
        int pageSize = 10;

        // ARRANGE
        var httpClient = _factory.CreateClient();
        
        var modelRecord = await ModelGenerator.PopulateDatabaseForPersonModelTest(httpClient);
        var list = new List<PersonModelResponse>();
        for(int i = 0; i < 5; i++)
        {
            var modelRequest = ModelGenerator.GenerateNewCreatePersonModelRequest(modelRecord);

            var create = await httpClient.PostAsJsonAsync(CreatePersonModelEndpoint.EndpointPrefix, modelRequest);
            var get = await httpClient.GetAsync(create.Headers.Location.AbsolutePath);
            var createdModel = await get.Content.ReadFromJsonAsync<PersonModelResponse>();
            _createdPersonModels.Add(createdModel.Id);
            list.Add(createdModel);
        }
        var control = new PersonModelsResponse() { Items = list, PageIndex = pageNumberForTest, PageSize = pageSize, TotalNumberOfAvailableResponses = numberOfModelsToMake };

        // ACT
        var getAllRequest = ModelGenerator.GenerateNewGetAllPersonModelRequest(pageNumberForTest, pageSize);
        string searchTerms = getAllRequest.ToSearchTermsString();
        var result = await httpClient.GetAsync($"{GetAllPersonModelEndpoint.EndpointPrefix}?{searchTerms}");
        var check = await result.Content.ReadFromJsonAsync<PersonModelsResponse>();

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        check.Should().BeEquivalentTo(control);
    }

    [Fact]
    public async Task GetAllPersonModels_ReturnsAllModelsAscending_WhenModelsExist()
    {
        int numberOfModelsToMake = 5;
        int pageNumberForTest = 0;
        int pageSize = 10;
        string sortTerm = "+Name";

        // ARRANGE
        var httpClient = _factory.CreateClient();

        var modelRecord = await ModelGenerator.PopulateDatabaseForPersonModelTest(httpClient);
        var list = new List<PersonModelResponse>();
        for (int i = 0; i < 5; i++)
        {
            var modelRequest = ModelGenerator.GenerateNewCreatePersonModelRequest(modelRecord);

            var create = await httpClient.PostAsJsonAsync(CreatePersonModelEndpoint.EndpointPrefix, modelRequest);
            var get = await httpClient.GetAsync(create.Headers.Location.AbsolutePath);
            var createdModel = await get.Content.ReadFromJsonAsync<PersonModelResponse>();
            _createdPersonModels.Add(createdModel.Id);
            list.Add(createdModel);
        }
        list = list.OrderBy( x=> x.Name ).ToList();
        var control = new PersonModelsResponse() { Items = list, PageIndex = pageNumberForTest, PageSize = pageSize, TotalNumberOfAvailableResponses = numberOfModelsToMake };

        // ACT
        var getAllRequest = ModelGenerator.GenerateNewGetAllPersonModelRequest(pageNumberForTest, pageSize, sortBy:sortTerm);
        string searchTerms = getAllRequest.ToSearchTermsString();
        var result = await httpClient.GetAsync($"{GetAllPersonModelEndpoint.EndpointPrefix}?{searchTerms}");
        var check = await result.Content.ReadFromJsonAsync<PersonModelsResponse>();

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        check.Should().BeEquivalentTo(control);
    }

    [Fact]
    public async Task GetAllPersonModels_ReturnsAllModelsDescending_WhenModelsExist()
    {
        int numberOfModelsToMake = 5;
        int pageNumberForTest = 0;
        int pageSize = 10;
        string sortTerm = "-Name";

        // ARRANGE
        var httpClient = _factory.CreateClient();

        var modelRecord = await ModelGenerator.PopulateDatabaseForPersonModelTest(httpClient);
        var list = new List<PersonModelResponse>();
        for (int i = 0; i < 5; i++)
        {
            var modelRequest = ModelGenerator.GenerateNewCreatePersonModelRequest(modelRecord);

            var create = await httpClient.PostAsJsonAsync(CreatePersonModelEndpoint.EndpointPrefix, modelRequest);
            var get = await httpClient.GetAsync(create.Headers.Location.AbsolutePath);
            var createdModel = await get.Content.ReadFromJsonAsync<PersonModelResponse>();
            _createdPersonModels.Add(createdModel.Id);
            list.Add(createdModel);
        }
        list = list.OrderByDescending(x => x.Name).ToList();
        var control = new PersonModelsResponse() { Items = list, PageIndex = pageNumberForTest, PageSize = pageSize, TotalNumberOfAvailableResponses = numberOfModelsToMake };

        // ACT
        var getAllRequest = ModelGenerator.GenerateNewGetAllPersonModelRequest(pageNumberForTest, pageSize, sortBy: sortTerm);
        string searchTerms = getAllRequest.ToSearchTermsString();
        var result = await httpClient.GetAsync($"{GetAllPersonModelEndpoint.EndpointPrefix}?{searchTerms}");
        var check = await result.Content.ReadFromJsonAsync<PersonModelsResponse>();

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        check.Should().BeEquivalentTo(control);
    }

    [Fact]
    public async Task GetAllPersonModels_ReturnsOK_WhenNoModelsExist()
    {
        // ARRANGE
        var httpClient = _factory.CreateClient();

        // ACT
        string searchTerms = "PageIndex=0&PageSize=10";
        var result = await httpClient.GetAsync($"{GetAllPersonModelEndpoint.EndpointPrefix}?{searchTerms}");
        var returnedModels = await result.Content.ReadFromJsonAsync<PersonModelsResponse>();

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        returnedModels.Items.Should().BeEmpty();
    }

    //[Fact]
    //public async Task SearchModels_ReturnsAllMatchingModels_When_____Matches()
    //{
    //    // ARRANGE
    //    var httpClient = _factory.CreateClient();

    //    // ACT

    //    // ASSERT
    //}
}
