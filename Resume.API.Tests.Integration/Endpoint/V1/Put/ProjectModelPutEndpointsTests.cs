using BeaniesUtilities.Models.Resume;
using FluentAssertions;
using Gay.TCazier.Resume.API;
using Gay.TCazier.Resume.API.Endpoints.V1.Create;
using Gay.TCazier.Resume.API.Endpoints.V1.Delete;
using Gay.TCazier.Resume.API.Endpoints.V1.Get;
using Gay.TCazier.Resume.API.Endpoints.V1.Put;
using Gay.TCazier.Resume.API.Mappings.V1;
using Gay.TCazier.Resume.Contracts.Requests.V1.Update;
using Gay.TCazier.Resume.Contracts.Responses.V1;
using Gay.TCazier.Resume.Contracts.Endpoints.V1;
using Resume.API.Tests.Integration.Mappings.V1;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace Resume.API.Tests.Integration.Endpoint.V1.Put;

public class ProjectModelPutEndpointsTests : IClassFixture<WebApplicationFactory<IAPIMarker>>, IAsyncLifetime
{
    private readonly WebApplicationFactory<IAPIMarker> _factory;

    private List<ProjectModelResponse> _createdProjectModels = new List<ProjectModelResponse>();

    public ProjectModelPutEndpointsTests(WebApplicationFactory<IAPIMarker> factory)
    {
        _factory = factory;
    }

    public async Task DisposeAsync()
    {
        var httpClient = _factory.CreateClient();
        foreach (int id in _createdProjectModels.Select(x=>x.Id))
        {
            await httpClient.DeleteAsync($"{ProjectModelEndpoints.EndpointPrefix}/{id}");
        }
    }

    public Task InitializeAsync() => Task.CompletedTask;

    [Fact]
    public async Task UpdateProjectModel_UpdatesModel_WhenDataIsCorrect()
    {
        // ARRANGE
        var httpClient = _factory.CreateClient();
        
        var propRecord = await ModelGenerator.PopulateDatabaseForProjectModelTest(httpClient);
        var modelRequest = ModelGenerator.GenerateNewCreateProjectModelRequest(propRecord);

        var create = await httpClient.PostAsJsonAsync(ProjectModelEndpoints.Post, modelRequest);
        var get = await httpClient.GetAsync(create.Headers.Location.AbsolutePath);
        var createdResponse = await get.Content.ReadFromJsonAsync<ProjectModelResponse>();
        _createdProjectModels.Add(createdResponse);

        // ACT
        UpdateProjectModelRequest updateRequest = createdResponse.MapToUpdateRequest();
        var result = await httpClient.PutAsJsonAsync($"{ProjectModelEndpoints.EndpointPrefix}/{createdResponse.Id}", updateRequest);
        get = await httpClient.GetAsync(result.Headers.Location.AbsolutePath);
        var updatedModel = await get.Content.ReadFromJsonAsync<ProjectModelResponse>();

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.Created);
        result.Headers.Location.AbsolutePath.Should().Be($"/{ProjectModelEndpoints.EndpointPrefix}/{updatedModel.Id}");

        updatedModel.Id.Should().Be(createdResponse.Id);
        updatedModel.Name.Should().NotBe(createdResponse.Name);

		updateRequest.Description.Should().Be(createdResponse.Description);
		updateRequest.Version.Should().Be(createdResponse.Version);
		updateRequest.ProjectUrl.Should().Be(createdResponse.ProjectUrl);
		updateRequest.StartDate.Should().Be(createdResponse.StartDate);
		updateRequest.CompletionDate.Should().Be(createdResponse.CompletionDate);
		updateRequest.TechTags.Should().BeEquivalentTo(createdResponse.TechTags.Select(x=>x.CommonIdentity).ToArray());
    }

    //public async Task UpdateProjectModel_DoesNotUpdateModel_WhenDataIsIncorrect()
    //{
    //    // ARRANGE
    //    var httpClient = _factory.CreateClient();
    //    //uhasdfgohjaoidfj
    //    var modelRequest = ModelGenerator.GenerateNewCreateProjectModelRequest();

    //    var create = await httpClient.PostAsJsonAsync(ProjectModelEndpoints.Post, modelRequest);
    //    var get = await httpClient.GetAsync(create.Headers.Location.AbsolutePath);
    //    var createdResponse = await create.Content.ReadFromJsonAsync<ProjectModelResponse>();
    //    _createdProjectModels.Add(createdResponse);

    //    // ACT
    //    UpdateProjectModelRequest updateRequest = ModelGenerator.GenerateNewUpdateProjectModelRequest(createdResponse);
    //    var result = await httpClient.GetAsync($"{ProjectModelEndpoints.EndpointPrefix}/{createdResponse.Id}");
    //    var updatedModel = await result.Content.ReadFromJsonAsync<ProjectModelResponse>();

    //    // ASSERT
    //    result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    //    updatedModel.Should().BeEquivalentTo(castModel);
    //    result.Headers.Location.Should().Be($"{ProjectModelEndpoints.EndpointPrefix}/{updatedModel.Id}");
    //}

    [Fact]
    public async Task UpdateProjectModel_ReturnsNotFound_WhenModelDoesNotExist()
    {
        // ARRANGE
        var httpClient = _factory.CreateClient();
        
        var propRecord = await ModelGenerator.PopulateDatabaseForProjectModelTest(httpClient);
        var modelRequest = ModelGenerator.GenerateNewCreateProjectModelRequest(propRecord);
        var fakedModel = modelRequest.MapToModelFromCreateRequest(-10000,"IntegrationTesting", new List<TechTagModel>());
        var fakedResponse = fakedModel.MapToResponseFromModel();

        // ACT
        UpdateProjectModelRequest updateRequest = fakedResponse.MapToUpdateRequest();
        var result = await httpClient.PutAsJsonAsync($"{ProjectModelEndpoints.EndpointPrefix}/{updateRequest.Id}", updateRequest);

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
