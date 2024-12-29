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

public class EducationDegreeModelGetEndpointsTests : IClassFixture<WebApplicationFactory<IAPIMarker>>, IAsyncLifetime
{
    private readonly WebApplicationFactory<IAPIMarker> _factory;

    private List<int> _createdEducationDegreeModels = new List<int>();

    public EducationDegreeModelGetEndpointsTests(WebApplicationFactory<IAPIMarker> factory)
    {
        _factory = factory;
    }

    public async Task DisposeAsync()
    {
        var httpClient = _factory.CreateClient();
        foreach (int id in _createdEducationDegreeModels)
        {
            await httpClient.DeleteAsync($"{DeleteEducationDegreeModelEndpoint.EndpointPrefix}/{id}");
        }
    }

    public Task InitializeAsync() => Task.CompletedTask;

    [Fact]
    public async Task GetEducationDegreeModel_ReturnsEducationDegreeModel_WhenModelExists()
    {
        // ARRANGE
        var httpClient = _factory.CreateClient();
        
        var modelRecord = await ModelGenerator.PopulateDatabaseForEducationDegreeModelTest(httpClient);
        var modelRequest = ModelGenerator.GenerateNewCreateEducationDegreeModelRequest(modelRecord);

        var create = await httpClient.PostAsJsonAsync(CreateEducationDegreeModelEndpoint.EndpointPrefix, modelRequest);
        var get = await httpClient.GetAsync(create.Headers.Location.AbsolutePath);
        var createdModel = await get.Content.ReadFromJsonAsync<EducationDegreeModel>();
        _createdEducationDegreeModels.Add(createdModel.CommonIdentity);

        // ACT
        var result = await httpClient.GetAsync($"{GetEducationDegreeModelEndpoint.EndpointPrefix}/{createdModel.CommonIdentity}");
        var foundModel = await result.Content.ReadFromJsonAsync<EducationDegreeModel>();

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        foundModel.Should().BeEquivalentTo(createdModel);
    }

    [Fact]
    public async Task GetEducationDegreeModel_ReturnsNotFound_WhenModelDoesNotExist()
    {
        // ARRANGE
        var httpClient = _factory.CreateClient();

        // ACT
        var result = await httpClient.GetAsync($"{GetEducationDegreeModelEndpoint.EndpointPrefix}/{-1000000}");

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetAllEducationDegreeModels_ReturnsAllModelsUnsorted_WhenModelsExist()
    {
        int numberOfModelsToMake = 5;
        int pageNumberForTest = 0;
        int pageSize = 10;

        // ARRANGE
        var httpClient = _factory.CreateClient();
        
        var modelRecord = await ModelGenerator.PopulateDatabaseForEducationDegreeModelTest(httpClient);
        var list = new List<EducationDegreeModelResponse>();
        for(int i = 0; i < 5; i++)
        {
            var modelRequest = ModelGenerator.GenerateNewCreateEducationDegreeModelRequest(modelRecord);

            var create = await httpClient.PostAsJsonAsync(CreateEducationDegreeModelEndpoint.EndpointPrefix, modelRequest);
            var get = await httpClient.GetAsync(create.Headers.Location.AbsolutePath);
            var createdModel = await get.Content.ReadFromJsonAsync<EducationDegreeModelResponse>();
            _createdEducationDegreeModels.Add(createdModel.Id);
            list.Add(createdModel);
        }
        var control = new EducationDegreeModelsResponse() { Items = list, PageIndex = pageNumberForTest, PageSize = pageSize, TotalNumberOfAvailableResponses = numberOfModelsToMake };

        // ACT
        var getAllRequest = ModelGenerator.GenerateNewGetAllEducationDegreeModelRequest(pageNumberForTest, pageSize);
        string searchTerms = getAllRequest.ToSearchTermsString();
        var result = await httpClient.GetAsync($"{GetAllEducationDegreeModelEndpoint.EndpointPrefix}?{searchTerms}");
        var check = await result.Content.ReadFromJsonAsync<EducationDegreeModelsResponse>();

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        check.Should().BeEquivalentTo(control);
    }

    [Fact]
    public async Task GetAllEducationDegreeModels_ReturnsAllModelsAscending_WhenModelsExist()
    {
        int numberOfModelsToMake = 5;
        int pageNumberForTest = 0;
        int pageSize = 10;
        string sortTerm = "+Name";

        // ARRANGE
        var httpClient = _factory.CreateClient();

        var modelRecord = await ModelGenerator.PopulateDatabaseForEducationDegreeModelTest(httpClient);
        var list = new List<EducationDegreeModelResponse>();
        for (int i = 0; i < 5; i++)
        {
            var modelRequest = ModelGenerator.GenerateNewCreateEducationDegreeModelRequest(modelRecord);

            var create = await httpClient.PostAsJsonAsync(CreateEducationDegreeModelEndpoint.EndpointPrefix, modelRequest);
            var get = await httpClient.GetAsync(create.Headers.Location.AbsolutePath);
            var createdModel = await get.Content.ReadFromJsonAsync<EducationDegreeModelResponse>();
            _createdEducationDegreeModels.Add(createdModel.Id);
            list.Add(createdModel);
        }
        list = list.OrderBy( x=> x.Name ).ToList();
        var control = new EducationDegreeModelsResponse() { Items = list, PageIndex = pageNumberForTest, PageSize = pageSize, TotalNumberOfAvailableResponses = numberOfModelsToMake };

        // ACT
        var getAllRequest = ModelGenerator.GenerateNewGetAllEducationDegreeModelRequest(pageNumberForTest, pageSize, sortBy:sortTerm);
        string searchTerms = getAllRequest.ToSearchTermsString();
        var result = await httpClient.GetAsync($"{GetAllEducationDegreeModelEndpoint.EndpointPrefix}?{searchTerms}");
        var check = await result.Content.ReadFromJsonAsync<EducationDegreeModelsResponse>();

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        check.Should().BeEquivalentTo(control);
    }

    [Fact]
    public async Task GetAllEducationDegreeModels_ReturnsAllModelsDescending_WhenModelsExist()
    {
        int numberOfModelsToMake = 5;
        int pageNumberForTest = 0;
        int pageSize = 10;
        string sortTerm = "-Name";

        // ARRANGE
        var httpClient = _factory.CreateClient();

        var modelRecord = await ModelGenerator.PopulateDatabaseForEducationDegreeModelTest(httpClient);
        var list = new List<EducationDegreeModelResponse>();
        for (int i = 0; i < 5; i++)
        {
            var modelRequest = ModelGenerator.GenerateNewCreateEducationDegreeModelRequest(modelRecord);

            var create = await httpClient.PostAsJsonAsync(CreateEducationDegreeModelEndpoint.EndpointPrefix, modelRequest);
            var get = await httpClient.GetAsync(create.Headers.Location.AbsolutePath);
            var createdModel = await get.Content.ReadFromJsonAsync<EducationDegreeModelResponse>();
            _createdEducationDegreeModels.Add(createdModel.Id);
            list.Add(createdModel);
        }
        list = list.OrderByDescending(x => x.Name).ToList();
        var control = new EducationDegreeModelsResponse() { Items = list, PageIndex = pageNumberForTest, PageSize = pageSize, TotalNumberOfAvailableResponses = numberOfModelsToMake };

        // ACT
        var getAllRequest = ModelGenerator.GenerateNewGetAllEducationDegreeModelRequest(pageNumberForTest, pageSize, sortBy: sortTerm);
        string searchTerms = getAllRequest.ToSearchTermsString();
        var result = await httpClient.GetAsync($"{GetAllEducationDegreeModelEndpoint.EndpointPrefix}?{searchTerms}");
        var check = await result.Content.ReadFromJsonAsync<EducationDegreeModelsResponse>();

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        check.Should().BeEquivalentTo(control);
    }

    [Fact]
    public async Task GetAllEducationDegreeModels_ReturnsOK_WhenNoModelsExist()
    {
        // ARRANGE
        var httpClient = _factory.CreateClient();

        // ACT
        string searchTerms = "PageIndex=0&PageSize=10";
        var result = await httpClient.GetAsync($"{GetAllEducationDegreeModelEndpoint.EndpointPrefix}?{searchTerms}");
        var returnedModels = await result.Content.ReadFromJsonAsync<EducationDegreeModelsResponse>();

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
