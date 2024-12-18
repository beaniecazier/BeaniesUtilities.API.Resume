using BeaniesUtilities.Models.Resume;
using FluentAssertions;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace Resume.API.Tests.Integration;

public class ResumeEndpointsTests : IClassFixture<WebApplicationFactory<IAPIMarker>>
{
    private readonly WebApplicationFactory<IAPIMarker> _factory;

    public ResumeEndpointsTests(WebApplicationFactory<IAPIMarker> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task CreateAddress_CreatesAddress_WhenDataIsCorrect()
    {
        // ARRANGE
        var httpClient = _factory.CreateClient();
        var address = GenerateNewAddress();

        // ACT
        var result = await httpClient.PostAsJsonAsync(AddressEndpoints.BaseRoute, address);
        var createdAddress = await result.Content.ReadFromJsonAsync<AddressModel>();

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.Created);
        createdAddress.Should().BeEquivalentTo(createdAddress);
        result.Headers.Location.Should().Be($"{AddressEndpoints.BaseRoute}/{createdAddress.CommonIdentity}");
    }

    [Fact]
    public async Task CreateAddress_Fails_WhenHouseNumberIsInvalid()
    {
        // ARRANGE
        var httpClient = _factory.CreateClient();
        var address = GenerateNewAddress();

        // ACT
        var result = await httpClient.PostAsJsonAsync(AddressEndpoints.BaseRoute, address);
        var errors = await result.Content.ReadFromJsonAsync<IEnumerable<ValidationFailure>>();
        var error = errors!.Single();

        // ASSERT
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        error.PropertyName.Should().Be("HouseNumber");
        error.ErrorMessage.Should().Be("");
    }

    private EditibleAddressModel GenerateNewAddress()
    {
        return new EditibleAddressModel
        {
            HouseNumber = 17033,
            StreetName = "Pleasanton",
            StreetType = "Lane",
            City = "Beaverton",
            Region = string.Empty,
            State = "Oregon",
            Country = "USA",
            PostalCode = 97003,
            Zip4 = null,
            CrossStreetName = string.Empty,
            PrefixDirection = string.Empty,
            PrefixType = string.Empty,
            SuffixDirection = string.Empty,
            SuffixType = string.Empty,
        };
    }
}
