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

public class PhoneNumberModelGetEndpointsTests : IClassFixture<WebApplicationFactory<IAPIMarker>>, IAsyncLifetime
{
    private readonly WebApplicationFactory<IAPIMarker> _factory;

    private List<int> _createdPhoneNumberModels = new List<int>();

    public PhoneNumberModelGetEndpointsTests(WebApplicationFactory<IAPIMarker> factory)
    {
        _factory = factory;
    }

    public async Task DisposeAsync()
    {
        var httpClient = _factory.CreateClient();
        foreach (int id in _createdPhoneNumberModels)
        {
            await httpClient.DeleteAsync($"{DeletePhoneNumberModelEndpoint.EndpointPrefix}/{id}");
        }
    }

    public Task InitializeAsync() => Task.CompletedTask;

    [Fact]
    public async Task GetPhoneNumberModel_ReturnsPhoneNumberModel_WhenModelExists()
    {
        // ARRANGE
        var httpClient = _factory.CreateClient();
        
        var modelRecord = await ModelGenerator.PopulateDatabaseForPhoneNumberModelTest(httpClient);
        var modelRequest = ModelGenerator.GenerateNewCreatePhoneNumberModelRequest(modelRecord);

        var create = await httpClient.PostAsJsonAsync(CreatePhoneNumberModelEndpoint.EndpointPrefix, modelRequest);
        var get = await httpClient.GetAsync(create.Headers.Location.AbsolutePath);
        var createdModel = await get.Content.ReadFromJsonAsync<PhoneNumberModelResponse>();
        _createdPhoneNumberModels.Add(createdModel.Id);

        // ACT
        var result = await httpClient.GetAsync($"{GetPhoneNumberModelEndpoint.EndpointPrefix}/{createdModel.Id}");
        var foundModel = await result.Content.ReadFromJsonAsync<PhoneNumberModelResponse>();

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        foundModel.Should().BeEquivalentTo(createdModel);
    }

    [Fact]
    public async Task GetPhoneNumberModel_ReturnsNotFound_WhenModelDoesNotExist()
    {
        // ARRANGE
        var httpClient = _factory.CreateClient();

        // ACT
        var result = await httpClient.GetAsync($"{GetPhoneNumberModelEndpoint.EndpointPrefix}/{-1000000}");

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetAllPhoneNumberModels_ReturnsAllModelsUnsorted_WhenModelsExist()
    {
        int numberOfModelsToMake = 5;
        int pageNumberForTest = 0;
        int pageSize = 10;

        // ARRANGE
        var httpClient = _factory.CreateClient();
        
        var modelRecord = await ModelGenerator.PopulateDatabaseForPhoneNumberModelTest(httpClient);
        var list = new List<PhoneNumberModelResponse>();
        for(int i = 0; i < 5; i++)
        {
            var modelRequest = ModelGenerator.GenerateNewCreatePhoneNumberModelRequest(modelRecord);

            var create = await httpClient.PostAsJsonAsync(CreatePhoneNumberModelEndpoint.EndpointPrefix, modelRequest);
            var get = await httpClient.GetAsync(create.Headers.Location.AbsolutePath);
            var createdModel = await get.Content.ReadFromJsonAsync<PhoneNumberModelResponse>();
            _createdPhoneNumberModels.Add(createdModel.Id);
            list.Add(createdModel);
        }
        var control = new PhoneNumberModelsResponse() { Items = list, PageIndex = pageNumberForTest, PageSize = pageSize, TotalNumberOfAvailableResponses = numberOfModelsToMake };

        // ACT
        var getAllRequest = ModelGenerator.GenerateNewGetAllPhoneNumberModelRequest(pageNumberForTest, pageSize);
        string searchTerms = getAllRequest.ToSearchTermsString();
        var result = await httpClient.GetAsync($"{GetPhoneNumberModelEndpoint.EndpointPrefix}?{searchTerms}");
        var check = await result.Content.ReadFromJsonAsync<PhoneNumberModelsResponse>();

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        check.Should().BeEquivalentTo(control);
    }

    [Fact]
    public async Task GetAllPhoneNumberModels_ReturnsAllModelsAscending_WhenModelsExist()
    {
        int numberOfModelsToMake = 5;
        int pageNumberForTest = 0;
        int pageSize = 10;
        string sortTerm = "+Name";

        // ARRANGE
        var httpClient = _factory.CreateClient();

        var modelRecord = await ModelGenerator.PopulateDatabaseForPhoneNumberModelTest(httpClient);
        var list = new List<PhoneNumberModelResponse>();
        for (int i = 0; i < 5; i++)
        {
            var modelRequest = ModelGenerator.GenerateNewCreatePhoneNumberModelRequest(modelRecord);

            var create = await httpClient.PostAsJsonAsync(CreatePhoneNumberModelEndpoint.EndpointPrefix, modelRequest);
            var get = await httpClient.GetAsync(create.Headers.Location.AbsolutePath);
            var createdModel = await get.Content.ReadFromJsonAsync<PhoneNumberModelResponse>();
            _createdPhoneNumberModels.Add(createdModel.Id);
            list.Add(createdModel);
        }
        list = list.OrderBy( x=> x.Name ).ToList();
        var control = new PhoneNumberModelsResponse() { Items = list, PageIndex = pageNumberForTest, PageSize = pageSize, TotalNumberOfAvailableResponses = numberOfModelsToMake };

        // ACT
        var getAllRequest = ModelGenerator.GenerateNewGetAllPhoneNumberModelRequest(pageNumberForTest, pageSize, sortBy:sortTerm);
        string searchTerms = getAllRequest.ToSearchTermsString();
        var result = await httpClient.GetAsync($"{GetPhoneNumberModelEndpoint.EndpointPrefix}?{searchTerms}");
        var check = await result.Content.ReadFromJsonAsync<PhoneNumberModelsResponse>();

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        check.Should().BeEquivalentTo(control);
    }

    [Fact]
    public async Task GetAllPhoneNumberModels_ReturnsAllModelsDescending_WhenModelsExist()
    {
        int numberOfModelsToMake = 5;
        int pageNumberForTest = 0;
        int pageSize = 10;
        string sortTerm = "-Name";

        // ARRANGE
        var httpClient = _factory.CreateClient();

        var modelRecord = await ModelGenerator.PopulateDatabaseForPhoneNumberModelTest(httpClient);
        var list = new List<PhoneNumberModelResponse>();
        for (int i = 0; i < 5; i++)
        {
            var modelRequest = ModelGenerator.GenerateNewCreatePhoneNumberModelRequest(modelRecord);

            var create = await httpClient.PostAsJsonAsync(CreatePhoneNumberModelEndpoint.EndpointPrefix, modelRequest);
            var get = await httpClient.GetAsync(create.Headers.Location.AbsolutePath);
            var createdModel = await get.Content.ReadFromJsonAsync<PhoneNumberModelResponse>();
            _createdPhoneNumberModels.Add(createdModel.Id);
            list.Add(createdModel);
        }
        list = list.OrderByDescending(x => x.Name).ToList();
        var control = new PhoneNumberModelsResponse() { Items = list, PageIndex = pageNumberForTest, PageSize = pageSize, TotalNumberOfAvailableResponses = numberOfModelsToMake };

        // ACT
        var getAllRequest = ModelGenerator.GenerateNewGetAllPhoneNumberModelRequest(pageNumberForTest, pageSize, sortBy: sortTerm);
        string searchTerms = getAllRequest.ToSearchTermsString();
        var result = await httpClient.GetAsync($"{GetPhoneNumberModelEndpoint.EndpointPrefix}?{searchTerms}");
        var check = await result.Content.ReadFromJsonAsync<PhoneNumberModelsResponse>();

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        check.Should().BeEquivalentTo(control);
    }

    [Fact]
    public async Task GetAllPhoneNumberModels_ReturnsOK_WhenNoModelsExist()
    {
        // ARRANGE
        var httpClient = _factory.CreateClient();

        // ACT
        string searchTerms = "PageIndex=0&PageSize=10";
        var result = await httpClient.GetAsync($"{GetPhoneNumberModelEndpoint.EndpointPrefix}?{searchTerms}");
        var returnedModels = await result.Content.ReadFromJsonAsync<PhoneNumberModelsResponse>();

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
