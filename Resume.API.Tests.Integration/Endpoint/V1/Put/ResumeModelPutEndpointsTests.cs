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

public class ResumeModelPutEndpointsTests : IClassFixture<WebApplicationFactory<IAPIMarker>>, IAsyncLifetime
{
    private readonly WebApplicationFactory<IAPIMarker> _factory;

    private List<ResumeModelResponse> _createdResumeModels = new List<ResumeModelResponse>();

    public ResumeModelPutEndpointsTests(WebApplicationFactory<IAPIMarker> factory)
    {
        _factory = factory;
    }

    public async Task DisposeAsync()
    {
        var httpClient = _factory.CreateClient();
        foreach (int id in _createdResumeModels.Select(x=>x.Id))
        {
            await httpClient.DeleteAsync($"{ResumeModelEndpoints.EndpointPrefix}/{id}");
        }
    }

    public Task InitializeAsync() => Task.CompletedTask;

    [Fact]
    public async Task UpdateResumeModel_UpdatesModel_WhenDataIsCorrect()
    {
        // ARRANGE
        var httpClient = _factory.CreateClient();
        
        var propRecord = await ModelGenerator.PopulateDatabaseForResumeModelTest(httpClient);
        var modelRequest = ModelGenerator.GenerateNewCreateResumeModelRequest(propRecord);

        var create = await httpClient.PostAsJsonAsync(ResumeModelEndpoints.Post, modelRequest);
        var get = await httpClient.GetAsync(create.Headers.Location.AbsolutePath);
        var createdResponse = await get.Content.ReadFromJsonAsync<ResumeModelResponse>();
        _createdResumeModels.Add(createdResponse);

        // ACT
        UpdateResumeModelRequest updateRequest = createdResponse.MapToUpdateRequest();
        var result = await httpClient.PutAsJsonAsync($"{ResumeModelEndpoints.EndpointPrefix}/{createdResponse.Id}", updateRequest);
        get = await httpClient.GetAsync(result.Headers.Location.AbsolutePath);
        var updatedModel = await get.Content.ReadFromJsonAsync<ResumeModelResponse>();

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.Created);
        result.Headers.Location.AbsolutePath.Should().Be($"/{ResumeModelEndpoints.EndpointPrefix}/{updatedModel.Id}");

        updatedModel.Id.Should().Be(createdResponse.Id);
        updatedModel.Name.Should().NotBe(createdResponse.Name);

		updateRequest.HeroStatement.Should().Be(createdResponse.HeroStatement);
		updateRequest.Degrees.Should().BeEquivalentTo(createdResponse.Degrees.Select(x=>x.CommonIdentity).ToArray());
		updateRequest.Certificates.Should().BeEquivalentTo(createdResponse.Certificates.Select(x=>x.CommonIdentity).ToArray());
		updateRequest.WorkExperience.Should().BeEquivalentTo(createdResponse.WorkExperience.Select(x=>x.CommonIdentity).ToArray());
		updateRequest.Projects.Should().BeEquivalentTo(createdResponse.Projects.Select(x=>x.CommonIdentity).ToArray());
		updateRequest.PreferedName.Should().Be(createdResponse.PreferedName);
		updateRequest.Pronouns.Should().BeEquivalentTo(createdResponse.Pronouns);
		updateRequest.Emails.Should().BeEquivalentTo(createdResponse.Emails);
		updateRequest.Socials.Should().BeEquivalentTo(createdResponse.Socials);
		updateRequest.Addresses.Should().BeEquivalentTo(createdResponse.Addresses.Select(x=>x.CommonIdentity).ToArray());
		updateRequest.PhoneNumbers.Should().BeEquivalentTo(createdResponse.PhoneNumbers.Select(x=>x.CommonIdentity).ToArray());
    }

    //public async Task UpdateResumeModel_DoesNotUpdateModel_WhenDataIsIncorrect()
    //{
    //    // ARRANGE
    //    var httpClient = _factory.CreateClient();
    //    //uhasdfgohjaoidfj
    //    var modelRequest = ModelGenerator.GenerateNewCreateResumeModelRequest();

    //    var create = await httpClient.PostAsJsonAsync(ResumeModelEndpoints.Post, modelRequest);
    //    var get = await httpClient.GetAsync(create.Headers.Location.AbsolutePath);
    //    var createdResponse = await create.Content.ReadFromJsonAsync<ResumeModelResponse>();
    //    _createdResumeModels.Add(createdResponse);

    //    // ACT
    //    UpdateResumeModelRequest updateRequest = ModelGenerator.GenerateNewUpdateResumeModelRequest(createdResponse);
    //    var result = await httpClient.GetAsync($"{ResumeModelEndpoints.EndpointPrefix}/{createdResponse.Id}");
    //    var updatedModel = await result.Content.ReadFromJsonAsync<ResumeModelResponse>();

    //    // ASSERT
    //    result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    //    updatedModel.Should().BeEquivalentTo(castModel);
    //    result.Headers.Location.Should().Be($"{ResumeModelEndpoints.EndpointPrefix}/{updatedModel.Id}");
    //}

    [Fact]
    public async Task UpdateResumeModel_ReturnsNotFound_WhenModelDoesNotExist()
    {
        // ARRANGE
        var httpClient = _factory.CreateClient();
        
        var propRecord = await ModelGenerator.PopulateDatabaseForResumeModelTest(httpClient);
        var modelRequest = ModelGenerator.GenerateNewCreateResumeModelRequest(propRecord);
        var fakedModel = modelRequest.MapToModelFromCreateRequest(-10000,"IntegrationTesting", new List<EducationDegreeModel>(), new List<CertificateModel>(), new List<WorkExperienceModel>(), new List<ProjectModel>(), new List<AddressModel>(), new List<PhoneNumberModel>());
        var fakedResponse = fakedModel.MapToResponseFromModel();

        // ACT
        UpdateResumeModelRequest updateRequest = fakedResponse.MapToUpdateRequest();
        var result = await httpClient.PutAsJsonAsync($"{ResumeModelEndpoints.EndpointPrefix}/{updateRequest.Id}", updateRequest);

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
