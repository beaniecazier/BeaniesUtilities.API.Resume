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

public class PersonModelPostEndpointsTests : IClassFixture<WebApplicationFactory<CreatePersonModelEndpoint>>, IAsyncLifetime
{
    private readonly WebApplicationFactory<CreatePersonModelEndpoint> _factory;

    private List<int> _createdPersonModels = new List<int>();

    public PersonModelPostEndpointsTests(WebApplicationFactory<CreatePersonModelEndpoint> factory)
    {
        _factory = factory;
    }

    public async Task DisposeAsync()
    {
        var httpClient = _factory.CreateClient();
        foreach (int id in _createdPersonModels)
        {
            await httpClient.DeleteAsync($"{DeletePersonModelEndpoint.EndpointPrefix}/{id}");
        }
    }

    public Task InitializeAsync() => Task.CompletedTask;

    [Fact]
    public async Task CreatePersonModel_CreatesPersonModel_WhenDataIsCorrect()
    {
        // ARRANGE
        var httpClient = _factory.CreateClient();
        
        var modelRecord = await ModelGenerator.PopulateDatabaseForPersonModelTest(httpClient);
        var modelRequest = ModelGenerator.GenerateNewCreatePersonModelRequest(modelRecord);

        // ACT
        var result = await httpClient.PostAsJsonAsync(CreatePersonModelEndpoint.EndpointPrefix, modelRequest);
        var url = result.Headers.Location.AbsolutePath;
        var get = await httpClient.GetAsync(url);
        var createdModel = await get.Content.ReadFromJsonAsync<PersonModel>();
        _createdPersonModels.Add(createdModel.CommonIdentity);

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.Created);
        createdModel.Should().BeEquivalentTo(createdModel);
        result.Headers.Location.AbsolutePath.Should().Be($"/{GetPersonModelEndpoint.EndpointPrefix}/{createdModel.CommonIdentity}");
    }

    //[Fact]
    //public async Task CreatePersonModel_Fails_WhenHouseNumberIsInvalid()
    //{
    //    // ARRANGE
    //    var httpClient = _factory.CreateClient();
    //    var modelRequest = ModelGenerator.GenerateNewCreatePersonModelRequest();

    //    modelRequest.HouseNumber = null;

    //    // ACT
    //    var result = await httpClient.PostAsJsonAsync(CreatePersonModelEndpoint.EndpointPrefix, modelRequest);
    //    var errors = await result.Content.ReadFromJsonAsync<IEnumerable<ValidationFailure>>();
    //    var error = errors!.Single();

    //    // ASSERT
    //    result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    //    error.PropertyName.Should().Be("HouseNumber");
    //    error.ErrorMessage.Should().Be("");
    //}

    //[Fact]
    //public async Task CreatePersonModel_Fails_InternalServerError() => Task.CompletedTask;
}
