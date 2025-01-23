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

public class CertificateModelPutEndpointsTests : IClassFixture<WebApplicationFactory<IAPIMarker>>, IAsyncLifetime
{
    private readonly WebApplicationFactory<IAPIMarker> _factory;

    private List<CertificateModelResponse> _createdCertificateModels = new List<CertificateModelResponse>();

    public CertificateModelPutEndpointsTests(WebApplicationFactory<IAPIMarker> factory)
    {
        _factory = factory;
    }

    public async Task DisposeAsync()
    {
        var httpClient = _factory.CreateClient();
        foreach (int id in _createdCertificateModels.Select(x=>x.Id))
        {
            await httpClient.DeleteAsync($"{CertificateModelEndpoints.EndpointPrefix}/{id}");
        }
    }

    public Task InitializeAsync() => Task.CompletedTask;

    [Fact]
    public async Task UpdateCertificateModel_UpdatesModel_WhenDataIsCorrect()
    {
        // ARRANGE
        var httpClient = _factory.CreateClient();
        
        var propRecord = await ModelGenerator.PopulateDatabaseForCertificateModelTest(httpClient);
        var modelRequest = ModelGenerator.GenerateNewCreateCertificateModelRequest(propRecord);

        var create = await httpClient.PostAsJsonAsync(CertificateModelEndpoints.Post, modelRequest);
        var get = await httpClient.GetAsync(create.Headers.Location.AbsolutePath);
        var createdResponse = await get.Content.ReadFromJsonAsync<CertificateModelResponse>();
        _createdCertificateModels.Add(createdResponse);

        // ACT
        UpdateCertificateModelRequest updateRequest = createdResponse.MapToUpdateRequest();
        var result = await httpClient.PutAsJsonAsync($"{CertificateModelEndpoints.EndpointPrefix}/{createdResponse.Id}", updateRequest);
        get = await httpClient.GetAsync(result.Headers.Location.AbsolutePath);
        var updatedModel = await get.Content.ReadFromJsonAsync<CertificateModelResponse>();

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.Created);
        result.Headers.Location.AbsolutePath.Should().Be($"/{CertificateModelEndpoints.EndpointPrefix}/{updatedModel.Id}");

        updatedModel.Id.Should().Be(createdResponse.Id);
        updatedModel.Name.Should().NotBe(createdResponse.Name);

		updateRequest.IssueDate.Should().Be(createdResponse.IssueDate);
		updateRequest.Link.Should().Be(createdResponse.Link);
		updateRequest.PdfFileName.Should().Be(createdResponse.PdfFileName);
		updateRequest.Issuer.Should().Be(createdResponse.Issuer.CommonIdentity);
		updateRequest.CertificateID.Should().Be(createdResponse.CertificateID);
		updateRequest.ExpirationDate.Should().Be(createdResponse.ExpirationDate);
    }

    //public async Task UpdateCertificateModel_DoesNotUpdateModel_WhenDataIsIncorrect()
    //{
    //    // ARRANGE
    //    var httpClient = _factory.CreateClient();
    //    //uhasdfgohjaoidfj
    //    var modelRequest = ModelGenerator.GenerateNewCreateCertificateModelRequest();

    //    var create = await httpClient.PostAsJsonAsync(CertificateModelEndpoints.Post, modelRequest);
    //    var get = await httpClient.GetAsync(create.Headers.Location.AbsolutePath);
    //    var createdResponse = await create.Content.ReadFromJsonAsync<CertificateModelResponse>();
    //    _createdCertificateModels.Add(createdResponse);

    //    // ACT
    //    UpdateCertificateModelRequest updateRequest = ModelGenerator.GenerateNewUpdateCertificateModelRequest(createdResponse);
    //    var result = await httpClient.GetAsync($"{CertificateModelEndpoints.EndpointPrefix}/{createdResponse.Id}");
    //    var updatedModel = await result.Content.ReadFromJsonAsync<CertificateModelResponse>();

    //    // ASSERT
    //    result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    //    updatedModel.Should().BeEquivalentTo(castModel);
    //    result.Headers.Location.Should().Be($"{CertificateModelEndpoints.EndpointPrefix}/{updatedModel.Id}");
    //}

    [Fact]
    public async Task UpdateCertificateModel_ReturnsNotFound_WhenModelDoesNotExist()
    {
        // ARRANGE
        var httpClient = _factory.CreateClient();
        
        var propRecord = await ModelGenerator.PopulateDatabaseForCertificateModelTest(httpClient);
        var modelRequest = ModelGenerator.GenerateNewCreateCertificateModelRequest(propRecord);
        var fakedModel = modelRequest.MapToModelFromCreateRequest(-10000,"IntegrationTesting", new EducationInstitutionModel());
        var fakedResponse = fakedModel.MapToResponseFromModel();

        // ACT
        UpdateCertificateModelRequest updateRequest = fakedResponse.MapToUpdateRequest();
        var result = await httpClient.PutAsJsonAsync($"{CertificateModelEndpoints.EndpointPrefix}/{updateRequest.Id}", updateRequest);

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
