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

public class AddressModelPostEndpointsTests : IClassFixture<WebApplicationFactory<CreateAddressModelEndpoint>>, IAsyncLifetime
{
    private readonly WebApplicationFactory<CreateAddressModelEndpoint> _factory;

    private List<int> _createdAddressModels = new List<int>();

    public AddressModelPostEndpointsTests(WebApplicationFactory<CreateAddressModelEndpoint> factory)
    {
        _factory = factory;
    }

    public async Task DisposeAsync()
    {
        var httpClient = _factory.CreateClient();
        foreach (int id in _createdAddressModels)
        {
            await httpClient.DeleteAsync($"{DeleteAddressModelEndpoint.EndpointPrefix}/{id}");
        }
    }

    public Task InitializeAsync() => Task.CompletedTask;

    [Fact]
    public async Task CreateAddressModel_CreatesAddressModel_WhenDataIsCorrect()
    {
        // ARRANGE
        var httpClient = _factory.CreateClient();
        
        var modelRecord = await ModelGenerator.PopulateDatabaseForAddressModelTest(httpClient);
        var modelRequest = ModelGenerator.GenerateNewCreateAddressModelRequest(modelRecord);

        // ACT
        var result = await httpClient.PostAsJsonAsync(CreateAddressModelEndpoint.EndpointPrefix, modelRequest);
        var url = result.Headers.Location.AbsolutePath;
        var get = await httpClient.GetAsync(url);
        var createdModel = await get.Content.ReadFromJsonAsync<AddressModel>();
        _createdAddressModels.Add(createdModel.CommonIdentity);

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.Created);
        createdModel.Should().BeEquivalentTo(createdModel);
        result.Headers.Location.AbsolutePath.Should().Be($"/{GetAddressModelEndpoint.EndpointPrefix}/{createdModel.CommonIdentity}");
    }

    //[Fact]
    //public async Task CreateAddressModel_Fails_WhenHouseNumberIsInvalid()
    //{
    //    // ARRANGE
    //    var httpClient = _factory.CreateClient();
    //    var modelRequest = ModelGenerator.GenerateNewCreateAddressModelRequest();

    //    modelRequest.HouseNumber = null;

    //    // ACT
    //    var result = await httpClient.PostAsJsonAsync(CreateAddressModelEndpoint.EndpointPrefix, modelRequest);
    //    var errors = await result.Content.ReadFromJsonAsync<IEnumerable<ValidationFailure>>();
    //    var error = errors!.Single();

    //    // ASSERT
    //    result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    //    error.PropertyName.Should().Be("HouseNumber");
    //    error.ErrorMessage.Should().Be("");
    //}

    //[Fact]
    //public async Task CreateAddressModel_Fails_InternalServerError() => Task.CompletedTask;
}
