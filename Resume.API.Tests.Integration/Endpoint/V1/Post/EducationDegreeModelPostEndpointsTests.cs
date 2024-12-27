using BeaniesUtilities.Models.Resume;
using FluentAssertions;
using FluentValidation.Results;
using Gay.TCazier.Resume.API.Endpoints.V1.Create;
using Gay.TCazier.Resume.API.Endpoints.V1.Get;
using Gay.TCazier.Resume.API.Endpoints.V1.Delete;
using Gay.TCazier.Resume.Contracts.Requests.V1.Create;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace Resume.API.Tests.Integration.Endpoint.V1.Post;

public class EducationDegreeModelPostEndpointsTests : IClassFixture<WebApplicationFactory<CreateEducationDegreeModelEndpoint>>, IAsyncLifetime
{
    private readonly WebApplicationFactory<CreateEducationDegreeModelEndpoint> _factory;

    private List<int> _createdEducationDegreeModels = new List<int>();

    public EducationDegreeModelPostEndpointsTests(WebApplicationFactory<CreateEducationDegreeModelEndpoint> factory)
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
    public async Task CreateEducationDegreeModel_CreatesEducationDegreeModel_WhenDataIsCorrect()
    {
        // ARRANGE
        var httpClient = _factory.CreateClient();
        
        var modelRecord = await ModelGenerator.PopulateDatabaseForEducationDegreeModelTest(httpClient);
        var modelRequest = ModelGenerator.GenerateNewCreateEducationDegreeModelRequest(modelRecord);

        // ACT
        var result = await httpClient.PostAsJsonAsync(CreateEducationDegreeModelEndpoint.EndpointPrefix, modelRequest);
        var url = result.Headers.Location.AbsolutePath;
        var get = await httpClient.GetAsync(url);
        var createdModel = await get.Content.ReadFromJsonAsync<EducationDegreeModel>();
        _createdEducationDegreeModels.Add(createdModel.CommonIdentity);

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.Created);
        createdModel.Should().BeEquivalentTo(createdModel);
        result.Headers.Location.AbsolutePath.Should().Be($"/{GetEducationDegreeModelEndpoint.EndpointPrefix}/{createdModel.CommonIdentity}");
    }

    //[Fact]
    //public async Task CreateEducationDegreeModel_Fails_WhenHouseNumberIsInvalid()
    //{
    //    // ARRANGE
    //    var httpClient = _factory.CreateClient();
    //    var modelRequest = ModelGenerator.GenerateNewCreateEducationDegreeModelRequest();

    //    modelRequest.HouseNumber = null;

    //    // ACT
    //    var result = await httpClient.PostAsJsonAsync(CreateEducationDegreeModelEndpoint.EndpointPrefix, modelRequest);
    //    var errors = await result.Content.ReadFromJsonAsync<IEnumerable<ValidationFailure>>();
    //    var error = errors!.Single();

    //    // ASSERT
    //    result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    //    error.PropertyName.Should().Be("HouseNumber");
    //    error.ErrorMessage.Should().Be("");
    //}

    //[Fact]
    //public async Task CreateEducationDegreeModel_Fails_InternalServerError() => Task.CompletedTask;
}
