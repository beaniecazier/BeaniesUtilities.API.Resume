using BeaniesUtilities.Models.Resume;
using FluentAssertions;
using Gay.TCazier.Resume.API;
using Gay.TCazier.Resume.API.Endpoints.V1.Create;
using Gay.TCazier.Resume.API.Endpoints.V1.Delete;
using Gay.TCazier.Resume.API.Endpoints.V1.Get;
using Gay.TCazier.Resume.Contracts.Responses.V1;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace Resume.API.Tests.Integration.Endpoint.V1.Get;

public class CertificateModelGetEndpointsTests : IClassFixture<WebApplicationFactory<IAPIMarker>>, IAsyncLifetime
{
    private readonly WebApplicationFactory<IAPIMarker> _factory;

    private List<int> _createdCertificateModels = new List<int>();

    public CertificateModelGetEndpointsTests(WebApplicationFactory<IAPIMarker> factory)
    {
        _factory = factory;
    }

    public async Task DisposeAsync()
    {
        var httpClient = _factory.CreateClient();
        foreach (int id in _createdCertificateModels)
        {
            await httpClient.DeleteAsync($"{DeleteCertificateModelEndpoint.EndpointPrefix}/{id}");
        }
    }

    public Task InitializeAsync() => Task.CompletedTask;

    [Fact]
    public async Task GetCertificateModel_ReturnsCertificateModel_WhenModelExists()
    {
        // ARRANGE
        var httpClient = _factory.CreateClient();
        
        var modelRecord = await ModelGenerator.PopulateDatabaseForCertificateModelTest(httpClient);
        var modelRequest = ModelGenerator.GenerateNewCreateCertificateModelRequest(modelRecord);

        var create = await httpClient.PostAsJsonAsync(CreateCertificateModelEndpoint.EndpointPrefix, modelRequest);
        var get = await httpClient.GetAsync(create.Headers.Location.AbsolutePath);
        var createdModel = await get.Content.ReadFromJsonAsync<CertificateModel>();
        _createdCertificateModels.Add(createdModel.CommonIdentity);

        // ACT
        var result = await httpClient.GetAsync($"{GetCertificateModelEndpoint.EndpointPrefix}/{createdModel.CommonIdentity}");
        var foundModel = await result.Content.ReadFromJsonAsync<CertificateModel>();

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        foundModel.Should().BeEquivalentTo(createdModel);
    }

    [Fact]
    public async Task GetCertificateModel_ReturnsNotFound_WhenModelDoesNotExist()
    {
        // ARRANGE
        var httpClient = _factory.CreateClient();

        // ACT
        var result = await httpClient.GetAsync($"{GetCertificateModelEndpoint.EndpointPrefix}/{-1000000}");

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetAllCertificateModels_ReturnsAllModels_WhenModelsExist()
    {
        int numberOfModelsToMake = 5;

        // ARRANGE
        var httpClient = _factory.CreateClient();
        
        var modelRecord = await ModelGenerator.PopulateDatabaseForCertificateModelTest(httpClient);
        var list = new List<CertificateModelResponse>();
        for(int i = 0; i < 5; i++)
        {
            var modelRequest = ModelGenerator.GenerateNewCreateCertificateModelRequest(modelRecord);

            var create = await httpClient.PostAsJsonAsync(CreateCertificateModelEndpoint.EndpointPrefix, modelRequest);
            var get = await httpClient.GetAsync(create.Headers.Location.AbsolutePath);
            var createdModel = await get.Content.ReadFromJsonAsync<CertificateModelResponse>();
            _createdCertificateModels.Add(createdModel.Id);
            list.Add(createdModel);
        }
        var control = new CertificateModelsResponse() { Items = list, PageIndex = 0, PageSize = 10, TotalNumberOfAvailableResponses = numberOfModelsToMake };

        // ACT
        var getAllRequest = ModelGenerator.GenerateNewGetAllCertificateModelRequest();
        string searchTerms = getAllRequest.ToSearchTermsString();
        var result = await httpClient.GetAsync($"{GetAllCertificateModelEndpoint.EndpointPrefix}?{searchTerms}");
        var check = await result.Content.ReadFromJsonAsync<CertificateModelsResponse>();

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        check.Should().BeEquivalentTo(control);
    }

    [Fact]
    public async Task GetAllCertificateModels_ReturnsOK_WhenNoModelsExist()
    {
        // ARRANGE
        var httpClient = _factory.CreateClient();

        // ACT
        var result = await httpClient.GetAsync(GetAllCertificateModelEndpoint.EndpointPrefix);
        var returnedModels = await result.Content.ReadFromJsonAsync<List<CertificateModel>>();

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        returnedModels.Should().BeEmpty();
    }

    //[Fact]
    //public async Task SearchModels_ReturnsAllMatchingModels_When_____Matches()
    //{
    //    // ARRANGE
    //    var httpClient = _factory.CreateClient();

    //    // ACT

    //    // ASSERT
    //}
}
