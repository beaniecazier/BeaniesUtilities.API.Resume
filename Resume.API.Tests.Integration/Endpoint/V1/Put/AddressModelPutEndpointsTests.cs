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

public class AddressModelPutEndpointsTests : IClassFixture<WebApplicationFactory<IAPIMarker>>, IAsyncLifetime
{
    private readonly WebApplicationFactory<IAPIMarker> _factory;

    private List<AddressModelResponse> _createdAddressModels = new List<AddressModelResponse>();

    public AddressModelPutEndpointsTests(WebApplicationFactory<IAPIMarker> factory)
    {
        _factory = factory;
    }

    public async Task DisposeAsync()
    {
        var httpClient = _factory.CreateClient();
        foreach (int id in _createdAddressModels.Select(x=>x.Id))
        {
            await httpClient.DeleteAsync($"{AddressModelEndpoints.EndpointPrefix}/{id}");
        }
    }

    public Task InitializeAsync() => Task.CompletedTask;

    [Fact]
    public async Task UpdateAddressModel_UpdatesModel_WhenDataIsCorrect()
    {
        // ARRANGE
        var httpClient = _factory.CreateClient();
        
        var propRecord = await ModelGenerator.PopulateDatabaseForAddressModelTest(httpClient);
        var modelRequest = ModelGenerator.GenerateNewCreateAddressModelRequest(propRecord);

        var create = await httpClient.PostAsJsonAsync(AddressModelEndpoints.Post, modelRequest);
        var get = await httpClient.GetAsync(create.Headers.Location.AbsolutePath);
        var createdResponse = await get.Content.ReadFromJsonAsync<AddressModelResponse>();
        _createdAddressModels.Add(createdResponse);

        // ACT
        UpdateAddressModelRequest updateRequest = createdResponse.MapToUpdateRequest();
        var result = await httpClient.PutAsJsonAsync($"{AddressModelEndpoints.EndpointPrefix}/{createdResponse.Id}", updateRequest);
        get = await httpClient.GetAsync(result.Headers.Location.AbsolutePath);
        var updatedModel = await get.Content.ReadFromJsonAsync<AddressModelResponse>();

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.Created);
        result.Headers.Location.AbsolutePath.Should().Be($"/{AddressModelEndpoints.EndpointPrefix}/{updatedModel.Id}");

        updatedModel.Id.Should().Be(createdResponse.Id);
        updatedModel.Name.Should().NotBe(createdResponse.Name);

		updateRequest.HouseNumber.Should().Be(createdResponse.HouseNumber);
		updateRequest.StreetName.Should().Be(createdResponse.StreetName);
		updateRequest.StreetType.Should().Be(createdResponse.StreetType);
		updateRequest.City.Should().Be(createdResponse.City);
		updateRequest.Region.Should().Be(createdResponse.Region);
		updateRequest.State.Should().Be(createdResponse.State);
		updateRequest.Country.Should().Be(createdResponse.Country);
		updateRequest.PostalCode.Should().Be(createdResponse.PostalCode);
		updateRequest.Zip4.Should().Be(createdResponse.Zip4);
		updateRequest.CrossStreetName.Should().Be(createdResponse.CrossStreetName);
		updateRequest.PrefixDirection.Should().Be(createdResponse.PrefixDirection);
		updateRequest.PrefixType.Should().Be(createdResponse.PrefixType);
		updateRequest.SuffixDirection.Should().Be(createdResponse.SuffixDirection);
		updateRequest.SuffixType.Should().Be(createdResponse.SuffixType);
    }

    //public async Task UpdateAddressModel_DoesNotUpdateModel_WhenDataIsIncorrect()
    //{
    //    // ARRANGE
    //    var httpClient = _factory.CreateClient();
    //    //uhasdfgohjaoidfj
    //    var modelRequest = ModelGenerator.GenerateNewCreateAddressModelRequest();

    //    var create = await httpClient.PostAsJsonAsync(AddressModelEndpoints.Post, modelRequest);
    //    var get = await httpClient.GetAsync(create.Headers.Location.AbsolutePath);
    //    var createdResponse = await create.Content.ReadFromJsonAsync<AddressModelResponse>();
    //    _createdAddressModels.Add(createdResponse);

    //    // ACT
    //    UpdateAddressModelRequest updateRequest = ModelGenerator.GenerateNewUpdateAddressModelRequest(createdResponse);
    //    var result = await httpClient.GetAsync($"{AddressModelEndpoints.EndpointPrefix}/{createdResponse.Id}");
    //    var updatedModel = await result.Content.ReadFromJsonAsync<AddressModelResponse>();

    //    // ASSERT
    //    result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    //    updatedModel.Should().BeEquivalentTo(castModel);
    //    result.Headers.Location.Should().Be($"{AddressModelEndpoints.EndpointPrefix}/{updatedModel.Id}");
    //}

    [Fact]
    public async Task UpdateAddressModel_ReturnsNotFound_WhenModelDoesNotExist()
    {
        // ARRANGE
        var httpClient = _factory.CreateClient();
        
        var propRecord = await ModelGenerator.PopulateDatabaseForAddressModelTest(httpClient);
        var modelRequest = ModelGenerator.GenerateNewCreateAddressModelRequest(propRecord);
        var fakedModel = modelRequest.MapToModelFromCreateRequest(-10000,"IntegrationTesting");
        var fakedResponse = fakedModel.MapToResponseFromModel();

        // ACT
        UpdateAddressModelRequest updateRequest = fakedResponse.MapToUpdateRequest();
        var result = await httpClient.PutAsJsonAsync($"{AddressModelEndpoints.EndpointPrefix}/{updateRequest.Id}", updateRequest);

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
