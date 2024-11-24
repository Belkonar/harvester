using harvester_agent.Workers;

namespace harvester_agent;

public class Worker(ILogger<Worker> logger, MainWorker worker) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            await worker.Run();

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }
}