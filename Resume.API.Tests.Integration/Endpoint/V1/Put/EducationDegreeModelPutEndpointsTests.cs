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

public class EducationDegreeModelPutEndpointsTests : IClassFixture<WebApplicationFactory<IAPIMarker>>, IAsyncLifetime
{
    private readonly WebApplicationFactory<IAPIMarker> _factory;

    private List<EducationDegreeModelResponse> _createdEducationDegreeModels = new List<EducationDegreeModelResponse>();

    public EducationDegreeModelPutEndpointsTests(WebApplicationFactory<IAPIMarker> factory)
    {
        _factory = factory;
    }

    public async Task DisposeAsync()
    {
        var httpClient = _factory.CreateClient();
        foreach (int id in _createdEducationDegreeModels.Select(x=>x.Id))
        {
            await httpClient.DeleteAsync($"{DeleteEducationDegreeModelEndpoint.EndpointPrefix}/{id}");
        }
    }

    public Task InitializeAsync() => Task.CompletedTask;

    [Fact]
    public async Task UpdateEducationDegreeModel_UpdatesModel_WhenDataIsCorrect()
    {
        // ARRANGE
        var httpClient = _factory.CreateClient();
        
        var propRecord = await ModelGenerator.PopulateDatabaseForEducationDegreeModelTest(httpClient);
        var modelRequest = ModelGenerator.GenerateNewCreateEducationDegreeModelRequest(propRecord);

        var create = await httpClient.PostAsJsonAsync(CreateEducationDegreeModelEndpoint.EndpointPrefix, modelRequest);
        var get = await httpClient.GetAsync(create.Headers.Location.AbsolutePath);
        var createdResponse = await get.Content.ReadFromJsonAsync<EducationDegreeModelResponse>();
        _createdEducationDegreeModels.Add(createdResponse);

        // ACT
        UpdateEducationDegreeModelRequest updateRequest = createdResponse.MapToUpdateRequest();
        var result = await httpClient.PutAsJsonAsync($"{UpdateEducationDegreeModelEndpoint.EndpointPrefix}/{createdResponse.Id}", updateRequest);
        get = await httpClient.GetAsync(result.Headers.Location.AbsolutePath);
        var updatedModel = await get.Content.ReadFromJsonAsync<EducationDegreeModelResponse>();

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.Created);
        result.Headers.Location.AbsolutePath.Should().Be($"/{GetEducationDegreeModelEndpoint.EndpointPrefix}/{updatedModel.Id}");

        updatedModel.Id.Should().Be(createdResponse.Id);
        updatedModel.Name.Should().NotBe(createdResponse.Name);

		updateRequest.GPA.Should().Be(createdResponse.GPA);
		updateRequest.StartDate.Should().Be(createdResponse.StartDate);
		updateRequest.EndDate.Should().Be(createdResponse.EndDate);
		updateRequest.Institution.Should().Be(createdResponse.Institution.CommonIdentity);
    }

    //public async Task UpdateEducationDegreeModel_DoesNotUpdateModel_WhenDataIsIncorrect()
    //{
    //    // ARRANGE
    //    var httpClient = _factory.CreateClient();
    //    //uhasdfgohjaoidfj
    //    var modelRequest = ModelGenerator.GenerateNewCreateEducationDegreeModelRequest();

    //    var create = await httpClient.PostAsJsonAsync(CreateEducationDegreeModelEndpoint.EndpointPrefix, modelRequest);
    //    var get = await httpClient.GetAsync(create.Headers.Location.AbsolutePath);
    //    var createdResponse = await create.Content.ReadFromJsonAsync<EducationDegreeModelResponse>();
    //    _createdEducationDegreeModels.Add(createdResponse);

    //    // ACT
    //    UpdateEducationDegreeModelRequest updateRequest = ModelGenerator.GenerateNewUpdateEducationDegreeModelRequest(createdResponse);
    //    var result = await httpClient.GetAsync($"{GetEducationDegreeModelEndpoint.EndpointPrefix}/{createdResponse.Id}");
    //    var updatedModel = await result.Content.ReadFromJsonAsync<EducationDegreeModelResponse>();

    //    // ASSERT
    //    result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    //    updatedModel.Should().BeEquivalentTo(castModel);
    //    result.Headers.Location.Should().Be($"{GetEducationDegreeModelEndpoint.EndpointPrefix}/{updatedModel.Id}");
    //}

    [Fact]
    public async Task UpdateEducationDegreeModel_ReturnsNotFound_WhenModelDoesNotExist()
    {
        // ARRANGE
        var httpClient = _factory.CreateClient();
        
        var propRecord = await ModelGenerator.PopulateDatabaseForEducationDegreeModelTest(httpClient);
        var modelRequest = ModelGenerator.GenerateNewCreateEducationDegreeModelRequest(propRecord);
        var fakedModel = modelRequest.MapToModelFromCreateRequest(-10000,"IntegrationTesting", new EducationInstitutionModel());
        var fakedResponse = fakedModel.MapToResponseFromModel();

        // ACT
        UpdateEducationDegreeModelRequest updateRequest = fakedResponse.MapToUpdateRequest();
        var result = await httpClient.PutAsJsonAsync($"{UpdateEducationDegreeModelEndpoint.EndpointPrefix}/{updateRequest.Id}", updateRequest);

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
