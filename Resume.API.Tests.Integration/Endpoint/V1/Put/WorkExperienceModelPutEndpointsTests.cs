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
using Resume.API.Tests.Integration.Mappings.V1;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace Resume.API.Tests.Integration.Endpoint.V1.Put;

public class WorkExperienceModelPutEndpointsTests : IClassFixture<WebApplicationFactory<IAPIMarker>>, IAsyncLifetime
{
    private readonly WebApplicationFactory<IAPIMarker> _factory;

    private List<WorkExperienceModelResponse> _createdWorkExperienceModels = new List<WorkExperienceModelResponse>();

    public WorkExperienceModelPutEndpointsTests(WebApplicationFactory<IAPIMarker> factory)
    {
        _factory = factory;
    }

    public async Task DisposeAsync()
    {
        var httpClient = _factory.CreateClient();
        foreach (int id in _createdWorkExperienceModels.Select(x=>x.Id))
        {
            await httpClient.DeleteAsync($"{DeleteWorkExperienceModelEndpoint.EndpointPrefix}/{id}");
        }
    }

    public Task InitializeAsync() => Task.CompletedTask;

    [Fact]
    public async Task UpdateWorkExperienceModel_UpdatesModel_WhenDataIsCorrect()
    {
        // ARRANGE
        var httpClient = _factory.CreateClient();
        
        var propRecord = await ModelGenerator.PopulateDatabaseForWorkExperienceModelTest(httpClient);
        var modelRequest = ModelGenerator.GenerateNewCreateWorkExperienceModelRequest(propRecord);

        var create = await httpClient.PostAsJsonAsync(CreateWorkExperienceModelEndpoint.EndpointPrefix, modelRequest);
        var get = await httpClient.GetAsync(create.Headers.Location.AbsolutePath);
        var createdResponse = await get.Content.ReadFromJsonAsync<WorkExperienceModelResponse>();
        _createdWorkExperienceModels.Add(createdResponse);

        // ACT
        UpdateWorkExperienceModelRequest updateRequest = createdResponse.MapToUpdateRequest();
        var result = await httpClient.PutAsJsonAsync($"{UpdateWorkExperienceModelEndpoint.EndpointPrefix}/{createdResponse.Id}", updateRequest);
        get = await httpClient.GetAsync(result.Headers.Location.AbsolutePath);
        var updatedModel = await get.Content.ReadFromJsonAsync<WorkExperienceModelResponse>();

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.Created);
        result.Headers.Location.AbsolutePath.Should().Be($"/{GetWorkExperienceModelEndpoint.EndpointPrefix}/{updatedModel.Id}");

        updatedModel.Id.Should().Be(createdResponse.Id);
        updatedModel.Name.Should().NotBe(createdResponse.Name);

		updateRequest.StartDate.Should().Be(createdResponse.StartDate);
		updateRequest.EndDate.Should().Be(createdResponse.EndDate);
		updateRequest.Company.Should().Be(createdResponse.Company);
		updateRequest.Description.Should().Be(createdResponse.Description);
		updateRequest.Responsibilities.Should().BeEquivalentTo(createdResponse.Responsibilities);
		updateRequest.TechUsed.Should().BeEquivalentTo(createdResponse.TechUsed.Select(x=>x.CommonIdentity).ToArray());
    }

    //public async Task UpdateWorkExperienceModel_DoesNotUpdateModel_WhenDataIsIncorrect()
    //{
    //    // ARRANGE
    //    var httpClient = _factory.CreateClient();
    //    //uhasdfgohjaoidfj
    //    var modelRequest = ModelGenerator.GenerateNewCreateWorkExperienceModelRequest();

    //    var create = await httpClient.PostAsJsonAsync(CreateWorkExperienceModelEndpoint.EndpointPrefix, modelRequest);
    //    var get = await httpClient.GetAsync(create.Headers.Location.AbsolutePath);
    //    var createdResponse = await create.Content.ReadFromJsonAsync<WorkExperienceModelResponse>();
    //    _createdWorkExperienceModels.Add(createdResponse);

    //    // ACT
    //    UpdateWorkExperienceModelRequest updateRequest = ModelGenerator.GenerateNewUpdateWorkExperienceModelRequest(createdResponse);
    //    var result = await httpClient.GetAsync($"{GetWorkExperienceModelEndpoint.EndpointPrefix}/{createdResponse.Id}");
    //    var updatedModel = await result.Content.ReadFromJsonAsync<WorkExperienceModelResponse>();

    //    // ASSERT
    //    result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    //    updatedModel.Should().BeEquivalentTo(castModel);
    //    result.Headers.Location.Should().Be($"{GetWorkExperienceModelEndpoint.EndpointPrefix}/{updatedModel.Id}");
    //}

    [Fact]
    public async Task UpdateWorkExperienceModel_ReturnsNotFound_WhenModelDoesNotExist()
    {
        // ARRANGE
        var httpClient = _factory.CreateClient();
        
        var propRecord = await ModelGenerator.PopulateDatabaseForWorkExperienceModelTest(httpClient);
        var modelRequest = ModelGenerator.GenerateNewCreateWorkExperienceModelRequest(propRecord);
        var fakedModel = modelRequest.MapToModelFromCreateRequest(-10000,"IntegrationTesting", new List<TechTagModel>());
        var fakedResponse = fakedModel.MapToResponseFromModel();

        // ACT
        UpdateWorkExperienceModelRequest updateRequest = fakedResponse.MapToUpdateRequest();
        var result = await httpClient.PutAsJsonAsync($"{UpdateWorkExperienceModelEndpoint.EndpointPrefix}/{updateRequest.Id}", updateRequest);

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
