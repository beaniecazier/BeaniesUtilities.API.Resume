using FluentAssertions;
using Gay.TCazier.Resume.API;
using Gay.TCazier.Resume.API.Endpoints.V1.Delete;
using Gay.TCazier.Resume.API.Endpoints.V1.Health;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net;

namespace Resume.API.Tests.Integration;

public class HealthCheckEndpointTest : IClassFixture<WebApplicationFactory<IAPIMarker>>, IAsyncLifetime
{
    private readonly WebApplicationFactory<IAPIMarker> _factory;

    public HealthCheckEndpointTest(WebApplicationFactory<IAPIMarker> factory)
    {
        _factory = factory;
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() => Task.CompletedTask;

    [Fact]
    public async Task HealthCheck_ReturnsOK_WhenAllGoesWell()
    {
        // ARRANGE
        var httpClient = _factory.CreateClient();

        // ACT
        var first = await httpClient.GetAsync($"http://localhost:5001/{StartupCheckEndpoint.EndpointPrefix}/{StartupCheckEndpoint.Tag}");
        Thread.Sleep(15000);
        var afterStartup = await httpClient.GetAsync($"http://localhost:5001/{StartupCheckEndpoint.EndpointPrefix}/{StartupCheckEndpoint.Tag}");
        var ready = await httpClient.GetAsync($"http://localhost:5002/{ReadinessCheckEndpoint.EndpointPrefix}/{ReadinessCheckEndpoint.Tag}");
        for (var i = 0; i < 10; i++)
        {
            await Task.Delay(1000);
            var alive = await httpClient.GetAsync($"http://localhost:5003/{LivenessCheckEndpoint.EndpointPrefix}/{LivenessCheckEndpoint.Tag}");
            alive.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        // ASSERT
        first.StatusCode.Should().Be(HttpStatusCode.ServiceUnavailable);
        afterStartup.StatusCode.Should().Be(HttpStatusCode.OK);
        ready.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task HealthCheckStartup_ReturnsNotFound_WhenWrongPort()
    {
        // ARRANGE
        var httpClient = _factory.CreateClient();

        // ACT
        var first = await httpClient.GetAsync($"{StartupCheckEndpoint.EndpointPrefix}/{StartupCheckEndpoint.Tag}");
        Thread.Sleep(15000);
        var afterStartup = await httpClient.GetAsync($"{StartupCheckEndpoint.EndpointPrefix}/{StartupCheckEndpoint.Tag}");

        // ASSERT
        httpClient.BaseAddress!.Port.Should().NotBe(5002);
        first.StatusCode.Should().Be(HttpStatusCode.NotFound);
        afterStartup.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task HealthCheckReady_ReturnsNotFound_WhenWrongPort()
    {
        // ARRANGE
        var httpClient = _factory.CreateClient();

        // ACT
        await httpClient.GetAsync($"http://localhost:5001/{StartupCheckEndpoint.EndpointPrefix}/{StartupCheckEndpoint.Tag}");
        Thread.Sleep(15000);
        await httpClient.GetAsync($"http://localhost:5001/{StartupCheckEndpoint.EndpointPrefix}/{StartupCheckEndpoint.Tag}");
        var ready = await httpClient.GetAsync($"{ReadinessCheckEndpoint.EndpointPrefix}/{ReadinessCheckEndpoint.Tag}");

        // ASSERT
        httpClient.BaseAddress!.Port.Should().NotBe(5002);
        ready.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task HealthCheckLiveness_ReturnsNotFound_WhenWrongPort()
    {
        // ARRANGE
        var httpClient = _factory.CreateClient();

        // ACT
        var first = await httpClient.GetAsync($"http://localhost:5001/{StartupCheckEndpoint.EndpointPrefix}/{StartupCheckEndpoint.Tag}");
        Thread.Sleep(15000);
        var afterStartup = await httpClient.GetAsync($"http://localhost:5001/{StartupCheckEndpoint.EndpointPrefix}/{StartupCheckEndpoint.Tag}");
        var ready = await httpClient.GetAsync($"http://localhost:5002/{ReadinessCheckEndpoint.EndpointPrefix}/{ReadinessCheckEndpoint.Tag}");
        await Task.Delay(1000);
        var alive = await httpClient.GetAsync($"{LivenessCheckEndpoint.EndpointPrefix}/{LivenessCheckEndpoint.Tag}");

        // ASSERT
        first.StatusCode.Should().Be(HttpStatusCode.ServiceUnavailable);
        afterStartup.StatusCode.Should().Be(HttpStatusCode.OK);
        ready.StatusCode.Should().Be(HttpStatusCode.OK);
        httpClient.BaseAddress!.Port.Should().NotBe(5003);
        alive.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
