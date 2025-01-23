using Gay.TCazier.Resume.API.Endpoints.V1.Delete;
using Gay.TCazier.Resume.API;
using Gay.TCazier.Resume.Contracts.Responses.V1;
using Microsoft.AspNetCore.Mvc.Testing;
using Gay.TCazier.Resume.API.Endpoints.V1.Get;
using System.Net;
using FluentAssertions;
using Gay.TCazier.Resume.API.Endpoints.V1.Create;
using Gay.TCazier.Resume.Contracts.Endpoints.V1;

namespace Resume.API.Tests.Integration.Endpoint.V1.Delete;

public class EducationInstitutionModelDeleteEndpointsTest : IClassFixture<WebApplicationFactory<IAPIMarker>>, IAsyncLifetime
{
    private readonly WebApplicationFactory<IAPIMarker> _factory;

    private List<EducationInstitutionModelResponse> _createdEducationInstitutionModels = new List<EducationInstitutionModelResponse>();

    public EducationInstitutionModelDeleteEndpointsTest(WebApplicationFactory<IAPIMarker> factory)
    {
        _factory = factory;
    }

    public async Task DisposeAsync()
    {
        var httpClient = _factory.CreateClient();
        foreach (int id in _createdEducationInstitutionModels.Select(x => x.Id))
        {
            await httpClient.DeleteAsync($"{EducationInstitutionModelEndpoints.EndpointPrefix}/{id}");
        }
    }

    public Task InitializeAsync() => Task.CompletedTask;

    [Fact]
    public async Task DeleteEducationInstitutionModel_ReturnsOk_WhenModelDoesExist()
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
        var result = await httpClient.DeleteAsync($"{EducationInstitutionModelEndpoints.EndpointPrefix}/{createdResponse.Id}");

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task DeleteEducationInstitutionModel_ReturnsNotFound_WhenModelDoesNotExist()
    {
        // ARRANGE
        var httpClient = _factory.CreateClient();

        // ACT
        var result = await httpClient.DeleteAsync($"{EducationInstitutionModelEndpoints.EndpointPrefix}/{-1000000}");

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
