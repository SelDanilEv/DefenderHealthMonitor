using Defender.HealthMonitor.Application.Common.Interfaces;
using Defender.HealthMonitor.Application.DTOs;
using Defender.HealthMonitor.Application.Enums;
using Defender.HealthMonitor.Application.Helpers;
using Defender.HealthMonitor.Domain.Entities.AppHealth;
using Microsoft.Extensions.Hosting;

namespace Defender.HealthMonitor.Application.HostedServices;

public class TimedMonitorService : IHostedService
{
    private const int MonitorTimerMs = 
        //10 *
        60 *
        1000;

    private static AppHealth? _currentApp = null;

    private readonly IAppHealthService _appHealthService;

    public TimedMonitorService(IAppHealthService appHealthService)
    {
        _appHealthService = appHealthService;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        if (_currentApp == null)
        {
            Task.Run(async () => RunMonitoring(_appHealthService));
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private static async Task RunMonitoring(IAppHealthService appHealthService)
    {
        try
        {
            var instanceName = EnvVariableResolver.GetEnvironmentVariable(EnvVariable.InstanceName);

            _currentApp = await appHealthService.GetAppHealthByNameAsync(instanceName);

            while (_currentApp != null && _currentApp.Name == instanceName && _currentApp.IsTimeToMonitor)
            {
                appHealthService.MonitorAppHealthsAsync();
                Thread.Sleep(MonitorTimerMs);

                _currentApp = await appHealthService.GetAppHealthByNameAsync(instanceName);
            }
        }
        catch (Exception ex)
        {
            SimpleLogger.Log(ex);
        }
        finally
        {
            _currentApp = null;
        }
    }
}