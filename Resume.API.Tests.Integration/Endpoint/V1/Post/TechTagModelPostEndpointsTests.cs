using BeaniesUtilities.Models.Resume;
using FluentAssertions;
using FluentValidation.Results;
using Gay.TCazier.Resume.API.Endpoints.V1.Create;
using Gay.TCazier.Resume.API.Endpoints.V1.Get;
using Gay.TCazier.Resume.API.Endpoints.V1.Delete;
using Gay.TCazier.Resume.Contracts.Requests.V1.Create;
using Gay.TCazier.Resume.Contracts.Responses.V1;
using Gay.TCazier.Resume.Contracts.Endpoints.V1;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace Resume.API.Tests.Integration.Endpoint.V1.Post;

public class TechTagModelPostEndpointsTests : IClassFixture<WebApplicationFactory<CreateTechTagModelEndpoint>>, IAsyncLifetime
{
    private readonly WebApplicationFactory<CreateTechTagModelEndpoint> _factory;

    private List<int> _createdTechTagModels = new List<int>();

    public TechTagModelPostEndpointsTests(WebApplicationFactory<CreateTechTagModelEndpoint> factory)
    {
        _factory = factory;
    }

    public async Task DisposeAsync()
    {
        var httpClient = _factory.CreateClient();
        foreach (int id in _createdTechTagModels)
        {
            await httpClient.DeleteAsync($"{TechTagModelEndpoints.EndpointPrefix}/{id}");
        }
    }

    public Task InitializeAsync() => Task.CompletedTask;

    [Fact]
    public async Task CreateTechTagModel_CreatesTechTagModel_WhenDataIsCorrect()
    {
        // ARRANGE
        var httpClient = _factory.CreateClient();
        
        var modelRecord = await ModelGenerator.PopulateDatabaseForTechTagModelTest(httpClient);
        var modelRequest = ModelGenerator.GenerateNewCreateTechTagModelRequest(modelRecord);

        // ACT
        var result = await httpClient.PostAsJsonAsync(TechTagModelEndpoints.Post, modelRequest);
        var url = result.Headers.Location.AbsolutePath;
        var get = await httpClient.GetAsync(url);
        var createdModel = await get.Content.ReadFromJsonAsync<TechTagModelResponse>();
        _createdTechTagModels.Add(createdModel.Id);

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.Created);
        createdModel.Should().BeEquivalentTo(createdModel);
        result.Headers.Location.AbsolutePath.Should().Be($"/{TechTagModelEndpoints.EndpointPrefix}/{createdModel.Id}");
    }

    //[Fact]
    //public async Task CreateTechTagModel_Fails_WhenHouseNumberIsInvalid()
    //{
    //    // ARRANGE
    //    var httpClient = _factory.CreateClient();
    //    var modelRequest = ModelGenerator.GenerateNewCreateTechTagModelRequest();

    //    modelRequest.HouseNumber = null;

    //    // ACT
    //    var result = await httpClient.PostAsJsonAsync(TechTagModelEndpoints.Post, modelRequest);
    //    var errors = await result.Content.ReadFromJsonAsync<IEnumerable<ValidationFailure>>();
    //    var error = errors!.Single();

    //    // ASSERT
    //    result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    //    error.PropertyName.Should().Be("HouseNumber");
    //    error.ErrorMessage.Should().Be("");
    //}

    //[Fact]
    //public async Task CreateTechTagModel_Fails_InternalServerError() => Task.CompletedTask;
}
