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

public class CertificateModelPostEndpointsTests : IClassFixture<WebApplicationFactory<CreateCertificateModelEndpoint>>, IAsyncLifetime
{
    private readonly WebApplicationFactory<CreateCertificateModelEndpoint> _factory;

    private List<int> _createdCertificateModels = new List<int>();

    public CertificateModelPostEndpointsTests(WebApplicationFactory<CreateCertificateModelEndpoint> factory)
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
    public async Task CreateCertificateModel_CreatesCertificateModel_WhenDataIsCorrect()
    {
        // ARRANGE
        var httpClient = _factory.CreateClient();
        
        var modelRecord = await ModelGenerator.PopulateDatabaseForCertificateModelTest(httpClient);
        var modelRequest = ModelGenerator.GenerateNewCreateCertificateModelRequest(modelRecord);

        // ACT
        var result = await httpClient.PostAsJsonAsync(CreateCertificateModelEndpoint.EndpointPrefix, modelRequest);
        var url = result.Headers.Location.AbsolutePath;
        var get = await httpClient.GetAsync(url);
        var createdModel = await get.Content.ReadFromJsonAsync<CertificateModel>();
        _createdCertificateModels.Add(createdModel.CommonIdentity);

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.Created);
        createdModel.Should().BeEquivalentTo(createdModel);
        result.Headers.Location.AbsolutePath.Should().Be($"/{GetCertificateModelEndpoint.EndpointPrefix}/{createdModel.CommonIdentity}");
    }

    //[Fact]
    //public async Task CreateCertificateModel_Fails_WhenHouseNumberIsInvalid()
    //{
    //    // ARRANGE
    //    var httpClient = _factory.CreateClient();
    //    var modelRequest = ModelGenerator.GenerateNewCreateCertificateModelRequest();

    //    modelRequest.HouseNumber = null;

    //    // ACT
    //    var result = await httpClient.PostAsJsonAsync(CreateCertificateModelEndpoint.EndpointPrefix, modelRequest);
    //    var errors = await result.Content.ReadFromJsonAsync<IEnumerable<ValidationFailure>>();
    //    var error = errors!.Single();

    //    // ASSERT
    //    result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    //    error.PropertyName.Should().Be("HouseNumber");
    //    error.ErrorMessage.Should().Be("");
    //}

    //[Fact]
    //public async Task CreateCertificateModel_Fails_InternalServerError() => Task.CompletedTask;
}
