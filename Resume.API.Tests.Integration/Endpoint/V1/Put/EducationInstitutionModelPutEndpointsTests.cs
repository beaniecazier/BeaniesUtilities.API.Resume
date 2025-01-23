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

public class EducationInstitutionModelPutEndpointsTests : IClassFixture<WebApplicationFactory<IAPIMarker>>, IAsyncLifetime
{
    private readonly WebApplicationFactory<IAPIMarker> _factory;

    private List<EducationInstitutionModelResponse> _createdEducationInstitutionModels = new List<EducationInstitutionModelResponse>();

    public EducationInstitutionModelPutEndpointsTests(WebApplicationFactory<IAPIMarker> factory)
    {
        _factory = factory;
    }

    public async Task DisposeAsync()
    {
        var httpClient = _factory.CreateClient();
        foreach (int id in _createdEducationInstitutionModels.Select(x=>x.Id))
        {
            await httpClient.DeleteAsync($"{EducationInstitutionModelEndpoints.EndpointPrefix}/{id}");
        }
    }

    public Task InitializeAsync() => Task.CompletedTask;

    [Fact]
    public async Task UpdateEducationInstitutionModel_UpdatesModel_WhenDataIsCorrect()
    {
        // ARRANGE
        var httpClient = _factory.CreateClient();
        
        var propRecord = await ModelGenerator.PopulateDatabaseForEducationInstitutionModelTest(httpClient);
        var modelRequest = ModelGenerator.GenerateNewCreateEducationInstitutionModelRequest(propRecord);

        var create = await httpClient.PostAsJsonAsync(EducationInstitutionModelEndpoints.Post, modelRequest);
        var get = await httpClient.GetAsync(create.Headers.Location.AbsolutePath);
        var createdResponse = await get.Content.ReadFromJsonAsync<EducationInstitutionModelResponse>();
        _createdEducationInstitutionModels.Add(createdResponse);

        // ACT
        UpdateEducationInstitutionModelRequest updateRequest = createdResponse.MapToUpdateRequest();
        var result = await httpClient.PutAsJsonAsync($"{EducationInstitutionModelEndpoints.EndpointPrefix}/{createdResponse.Id}", updateRequest);
        get = await httpClient.GetAsync(result.Headers.Location.AbsolutePath);
        var updatedModel = await get.Content.ReadFromJsonAsync<EducationInstitutionModelResponse>();

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.Created);
        result.Headers.Location.AbsolutePath.Should().Be($"/{EducationInstitutionModelEndpoints.EndpointPrefix}/{updatedModel.Id}");

        updatedModel.Id.Should().Be(createdResponse.Id);
        updatedModel.Name.Should().NotBe(createdResponse.Name);

		updateRequest.Website.Should().Be(createdResponse.Website);
		updateRequest.Address.Should().Be(createdResponse.Address.CommonIdentity);
    }

    //public async Task UpdateEducationInstitutionModel_DoesNotUpdateModel_WhenDataIsIncorrect()
    //{
    //    // ARRANGE
    //    var httpClient = _factory.CreateClient();
    //    //uhasdfgohjaoidfj
    //    var modelRequest = ModelGenerator.GenerateNewCreateEducationInstitutionModelRequest();

    //    var create = await httpClient.PostAsJsonAsync(EducationInstitutionModelEndpoints.Post, modelRequest);
    //    var get = await httpClient.GetAsync(create.Headers.Location.AbsolutePath);
    //    var createdResponse = await create.Content.ReadFromJsonAsync<EducationInstitutionModelResponse>();
    //    _createdEducationInstitutionModels.Add(createdResponse);

    //    // ACT
    //    UpdateEducationInstitutionModelRequest updateRequest = ModelGenerator.GenerateNewUpdateEducationInstitutionModelRequest(createdResponse);
    //    var result = await httpClient.GetAsync($"{EducationInstitutionModelEndpoints.EndpointPrefix}/{createdResponse.Id}");
    //    var updatedModel = await result.Content.ReadFromJsonAsync<EducationInstitutionModelResponse>();

    //    // ASSERT
    //    result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    //    updatedModel.Should().BeEquivalentTo(castModel);
    //    result.Headers.Location.Should().Be($"{EducationInstitutionModelEndpoints.EndpointPrefix}/{updatedModel.Id}");
    //}

    [Fact]
    public async Task UpdateEducationInstitutionModel_ReturnsNotFound_WhenModelDoesNotExist()
    {
        // ARRANGE
        var httpClient = _factory.CreateClient();
        
        var propRecord = await ModelGenerator.PopulateDatabaseForEducationInstitutionModelTest(httpClient);
        var modelRequest = ModelGenerator.GenerateNewCreateEducationInstitutionModelRequest(propRecord);
        var fakedModel = modelRequest.MapToModelFromCreateRequest(-10000,"IntegrationTesting", new AddressModel());
        var fakedResponse = fakedModel.MapToResponseFromModel();

        // ACT
        UpdateEducationInstitutionModelRequest updateRequest = fakedResponse.MapToUpdateRequest();
        var result = await httpClient.PutAsJsonAsync($"{EducationInstitutionModelEndpoints.EndpointPrefix}/{updateRequest.Id}", updateRequest);

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
