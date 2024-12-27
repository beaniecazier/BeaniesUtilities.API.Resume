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

public class PersonModelPutEndpointsTests : IClassFixture<WebApplicationFactory<IAPIMarker>>, IAsyncLifetime
{
    private readonly WebApplicationFactory<IAPIMarker> _factory;

    private List<PersonModelResponse> _createdPersonModels = new List<PersonModelResponse>();

    public PersonModelPutEndpointsTests(WebApplicationFactory<IAPIMarker> factory)
    {
        _factory = factory;
    }

    public async Task DisposeAsync()
    {
        var httpClient = _factory.CreateClient();
        foreach (int id in _createdPersonModels.Select(x=>x.Id))
        {
            await httpClient.DeleteAsync($"{DeletePersonModelEndpoint.EndpointPrefix}/{id}");
        }
    }

    public Task InitializeAsync() => Task.CompletedTask;

    [Fact]
    public async Task UpdatePersonModel_UpdatesModel_WhenDataIsCorrect()
    {
        // ARRANGE
        var httpClient = _factory.CreateClient();
        
        var propRecord = await ModelGenerator.PopulateDatabaseForPersonModelTest(httpClient);
        var modelRequest = ModelGenerator.GenerateNewCreatePersonModelRequest(propRecord);

        var create = await httpClient.PostAsJsonAsync(CreatePersonModelEndpoint.EndpointPrefix, modelRequest);
        var get = await httpClient.GetAsync(create.Headers.Location.AbsolutePath);
        var createdResponse = await get.Content.ReadFromJsonAsync<PersonModelResponse>();
        _createdPersonModels.Add(createdResponse);

        // ACT
        UpdatePersonModelRequest updateRequest = createdResponse.MapToUpdateRequest();
        var result = await httpClient.PutAsJsonAsync($"{UpdatePersonModelEndpoint.EndpointPrefix}/{createdResponse.Id}", updateRequest);
        get = await httpClient.GetAsync(result.Headers.Location.AbsolutePath);
        var updatedModel = await get.Content.ReadFromJsonAsync<PersonModelResponse>();

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.Created);
        result.Headers.Location.AbsolutePath.Should().Be($"/{GetPersonModelEndpoint.EndpointPrefix}/{updatedModel.Id}");

        updatedModel.Id.Should().Be(createdResponse.Id);
        updatedModel.Name.Should().NotBe(createdResponse.Name);

		updateRequest.PreferedName.Should().Be(createdResponse.PreferedName);
		updateRequest.Pronouns.Should().BeEquivalentTo(createdResponse.Pronouns);
		updateRequest.Emails.Should().BeEquivalentTo(createdResponse.Emails);
		updateRequest.Socials.Should().BeEquivalentTo(createdResponse.Socials);
		updateRequest.Addresses.Should().BeEquivalentTo(createdResponse.Addresses.Select(x=>x.CommonIdentity).ToArray());
		updateRequest.PhoneNumbers.Should().BeEquivalentTo(createdResponse.PhoneNumbers.Select(x=>x.CommonIdentity).ToArray());
    }

    //public async Task UpdatePersonModel_DoesNotUpdateModel_WhenDataIsIncorrect()
    //{
    //    // ARRANGE
    //    var httpClient = _factory.CreateClient();
    //    //uhasdfgohjaoidfj
    //    var modelRequest = ModelGenerator.GenerateNewCreatePersonModelRequest();

    //    var create = await httpClient.PostAsJsonAsync(CreatePersonModelEndpoint.EndpointPrefix, modelRequest);
    //    var get = await httpClient.GetAsync(create.Headers.Location.AbsolutePath);
    //    var createdResponse = await create.Content.ReadFromJsonAsync<PersonModelResponse>();
    //    _createdPersonModels.Add(createdResponse);

    //    // ACT
    //    UpdatePersonModelRequest updateRequest = ModelGenerator.GenerateNewUpdatePersonModelRequest(createdResponse);
    //    var result = await httpClient.GetAsync($"{GetPersonModelEndpoint.EndpointPrefix}/{createdResponse.Id}");
    //    var updatedModel = await result.Content.ReadFromJsonAsync<PersonModelResponse>();

    //    // ASSERT
    //    result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    //    updatedModel.Should().BeEquivalentTo(castModel);
    //    result.Headers.Location.Should().Be($"{GetPersonModelEndpoint.EndpointPrefix}/{updatedModel.Id}");
    //}

    [Fact]
    public async Task UpdatePersonModel_ReturnsNotFound_WhenModelDoesNotExist()
    {
        // ARRANGE
        var httpClient = _factory.CreateClient();
        
        var propRecord = await ModelGenerator.PopulateDatabaseForPersonModelTest(httpClient);
        var modelRequest = ModelGenerator.GenerateNewCreatePersonModelRequest(propRecord);
        var fakedModel = modelRequest.MapToModelFromCreateRequest(-10000,"IntegrationTesting", new List<AddressModel>(), new List<PhoneNumberModel>());
        var fakedResponse = fakedModel.MapToResponseFromModel();

        // ACT
        UpdatePersonModelRequest updateRequest = fakedResponse.MapToUpdateRequest();
        var result = await httpClient.PutAsJsonAsync($"{UpdatePersonModelEndpoint.EndpointPrefix}/{updateRequest.Id}", updateRequest);

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
