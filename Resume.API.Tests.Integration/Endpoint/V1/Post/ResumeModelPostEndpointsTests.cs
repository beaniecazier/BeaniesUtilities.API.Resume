using BeaniesUtilities.Models.Resume;
using FluentAssertions;
using FluentValidation.Results;
using Gay.TCazier.Resume.API.Endpoints.V1.Create;
using Gay.TCazier.Resume.API.Endpoints.V1.Get;
using Gay.TCazier.Resume.API.Endpoints.V1.Delete;
using Gay.TCazier.Resume.Contracts.Requests.V1.Create;
using Gay.TCazier.Resume.Contracts.Responses.V1;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace Resume.API.Tests.Integration.Endpoint.V1.Post;

public class ResumeModelPostEndpointsTests : IClassFixture<WebApplicationFactory<CreateResumeModelEndpoint>>, IAsyncLifetime
{
    private readonly WebApplicationFactory<CreateResumeModelEndpoint> _factory;

    private List<int> _createdResumeModels = new List<int>();

    public ResumeModelPostEndpointsTests(WebApplicationFactory<CreateResumeModelEndpoint> factory)
    {
        _factory = factory;
    }

    public async Task DisposeAsync()
    {
        var httpClient = _factory.CreateClient();
        foreach (int id in _createdResumeModels)
        {
            await httpClient.DeleteAsync($"{DeleteResumeModelEndpoint.EndpointPrefix}/{id}");
        }
    }

    public Task InitializeAsync() => Task.CompletedTask;

    [Fact]
    public async Task CreateResumeModel_CreatesResumeModel_WhenDataIsCorrect()
    {
        // ARRANGE
        var httpClient = _factory.CreateClient();
        
        var modelRecord = await ModelGenerator.PopulateDatabaseForResumeModelTest(httpClient);
        var modelRequest = ModelGenerator.GenerateNewCreateResumeModelRequest(modelRecord);

        // ACT
        var result = await httpClient.PostAsJsonAsync(CreateResumeModelEndpoint.EndpointPrefix, modelRequest);
        var url = result.Headers.Location.AbsolutePath;
        var get = await httpClient.GetAsync(url);
        var createdModel = await get.Content.ReadFromJsonAsync<ResumeModelResponse>();
        _createdResumeModels.Add(createdModel.Id);

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.Created);
        createdModel.Should().BeEquivalentTo(createdModel);
        result.Headers.Location.AbsolutePath.Should().Be($"/{GetResumeModelEndpoint.EndpointPrefix}/{createdModel.Id}");
    }

    //[Fact]
    //public async Task CreateResumeModel_Fails_WhenHouseNumberIsInvalid()
    //{
    //    // ARRANGE
    //    var httpClient = _factory.CreateClient();
    //    var modelRequest = ModelGenerator.GenerateNewCreateResumeModelRequest();

    //    modelRequest.HouseNumber = null;

    //    // ACT
    //    var result = await httpClient.PostAsJsonAsync(CreateResumeModelEndpoint.EndpointPrefix, modelRequest);
    //    var errors = await result.Content.ReadFromJsonAsync<IEnumerable<ValidationFailure>>();
    //    var error = errors!.Single();

    //    // ASSERT
    //    result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    //    error.PropertyName.Should().Be("HouseNumber");
    //    error.ErrorMessage.Should().Be("");
    //}

    //[Fact]
    //public async Task CreateResumeModel_Fails_InternalServerError() => Task.CompletedTask;
}
