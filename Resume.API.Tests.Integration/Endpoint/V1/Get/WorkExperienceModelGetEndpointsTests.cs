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

public class WorkExperienceModelGetEndpointsTests : IClassFixture<WebApplicationFactory<IAPIMarker>>, IAsyncLifetime
{
    private readonly WebApplicationFactory<IAPIMarker> _factory;

    private List<int> _createdWorkExperienceModels = new List<int>();

    public WorkExperienceModelGetEndpointsTests(WebApplicationFactory<IAPIMarker> factory)
    {
        _factory = factory;
    }

    public async Task DisposeAsync()
    {
        var httpClient = _factory.CreateClient();
        foreach (int id in _createdWorkExperienceModels)
        {
            await httpClient.DeleteAsync($"{DeleteWorkExperienceModelEndpoint.EndpointPrefix}/{id}");
        }
    }

    public Task InitializeAsync() => Task.CompletedTask;

    [Fact]
    public async Task GetWorkExperienceModel_ReturnsWorkExperienceModel_WhenModelExists()
    {
        // ARRANGE
        var httpClient = _factory.CreateClient();
        
        var modelRecord = await ModelGenerator.PopulateDatabaseForWorkExperienceModelTest(httpClient);
        var modelRequest = ModelGenerator.GenerateNewCreateWorkExperienceModelRequest(modelRecord);

        var create = await httpClient.PostAsJsonAsync(CreateWorkExperienceModelEndpoint.EndpointPrefix, modelRequest);
        var get = await httpClient.GetAsync(create.Headers.Location.AbsolutePath);
        var createdModel = await get.Content.ReadFromJsonAsync<WorkExperienceModel>();
        _createdWorkExperienceModels.Add(createdModel.CommonIdentity);

        // ACT
        var result = await httpClient.GetAsync($"{GetWorkExperienceModelEndpoint.EndpointPrefix}/{createdModel.CommonIdentity}");
        var foundModel = await result.Content.ReadFromJsonAsync<WorkExperienceModel>();

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        foundModel.Should().BeEquivalentTo(createdModel);
    }

    [Fact]
    public async Task GetWorkExperienceModel_ReturnsNotFound_WhenModelDoesNotExist()
    {
        // ARRANGE
        var httpClient = _factory.CreateClient();

        // ACT
        var result = await httpClient.GetAsync($"{GetWorkExperienceModelEndpoint.EndpointPrefix}/{-1000000}");

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetAllWorkExperienceModels_ReturnsAllModels_WhenModelsExist()
    {
        int numberOfModelsToMake = 5;

        // ARRANGE
        var httpClient = _factory.CreateClient();
        
        var modelRecord = await ModelGenerator.PopulateDatabaseForWorkExperienceModelTest(httpClient);
        var list = new List<WorkExperienceModelResponse>();
        for(int i = 0; i < 5; i++)
        {
            var modelRequest = ModelGenerator.GenerateNewCreateWorkExperienceModelRequest(modelRecord);

            var create = await httpClient.PostAsJsonAsync(CreateWorkExperienceModelEndpoint.EndpointPrefix, modelRequest);
            var get = await httpClient.GetAsync(create.Headers.Location.AbsolutePath);
            var createdModel = await get.Content.ReadFromJsonAsync<WorkExperienceModelResponse>();
            _createdWorkExperienceModels.Add(createdModel.Id);
            list.Add(createdModel);
        }
        var control = new WorkExperienceModelsResponse() { Items = list, PageIndex = 0, PageSize = 10, TotalNumberOfAvailableResponses = numberOfModelsToMake };

        // ACT
        var getAllRequest = ModelGenerator.GenerateNewGetAllWorkExperienceModelRequest();
        string searchTerms = getAllRequest.ToSearchTermsString();
        var result = await httpClient.GetAsync($"{GetAllWorkExperienceModelEndpoint.EndpointPrefix}?{searchTerms}");
        var check = await result.Content.ReadFromJsonAsync<WorkExperienceModelsResponse>();

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        check.Should().BeEquivalentTo(control);
    }

    [Fact]
    public async Task GetAllWorkExperienceModels_ReturnsOK_WhenNoModelsExist()
    {
        // ARRANGE
        var httpClient = _factory.CreateClient();

        // ACT
        var result = await httpClient.GetAsync(GetAllWorkExperienceModelEndpoint.EndpointPrefix);
        var returnedModels = await result.Content.ReadFromJsonAsync<List<WorkExperienceModel>>();

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        returnedModels.Should().BeEmpty();
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
