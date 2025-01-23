using Gay.TCazier.Resume.API.Endpoints.V1.Health;

namespace Gay.TCazier.Resume.API.BackgroundServices;

public class StartupBackgroundService : BackgroundService
{
    private readonly StartupCheckEndpoint _healthCheck;

    public static int FakedStartupDurationInSeconds = 10;

    public StartupBackgroundService(StartupCheckEndpoint healthCheck)
        => _healthCheck = healthCheck;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Simulate the effect of a long-running task.
        await Task.Delay(TimeSpan.FromSeconds(FakedStartupDurationInSeconds), stoppingToken);

        _healthCheck.StartupCompleted = true;
    }
}