﻿namespace PeriodicTimerDemo.Web;

public class PeriodicService : BackgroundService
{
    private readonly PeriodicTimer _timer = new(TimeSpan.FromSeconds(1));
    private readonly ILogger<PeriodicService> _logger;

    public PeriodicService(ILogger<PeriodicService> logger)
    {
        _logger = logger;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // We still have to wait for this task to finish.
        // To start this in the background:
        // - Task.Run
        // - start services concurrently (configure startup)
        // - IHostedLifecycleService
        _logger.LogInformation("Init");
        await Task.Delay(3000);

        while (await _timer.WaitForNextTickAsync(stoppingToken) 
               && !stoppingToken.IsCancellationRequested)
        {
            // Tries to stay as close to the set delay as possible
            _logger.LogInformation(DateTimeOffset.UtcNow.ToString("O"));
        }
    }
}