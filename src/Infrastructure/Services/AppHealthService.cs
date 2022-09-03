using Defender.HealthMonitor.Application.Common.Interfaces;
using Defender.HealthMonitor.Application.Common.Interfaces.Repositories;
using Defender.HealthMonitor.Application.DTOs;
using Defender.HealthMonitor.Domain.Entities.AppHealth;
using Defender.HealthMonitor.Domain.Enums;
using Defender.HealthMonitor.Infrastructure.Clients.Monitor;

namespace Defender.HealthMonitor.Infrastructure.Services;

public class AppHealthService : IAppHealthService
{
    private readonly IAppHealthRepository _appHealthRepository;
    private readonly IMonitorClient _monitorClient;

    public AppHealthService(
        IAppHealthRepository appHealthRepository,
        IMonitorClient monitorClient)
    {
        _appHealthRepository = appHealthRepository;
        _monitorClient = monitorClient;
    }

    public async Task<List<AppHealth>> GetAppHealthsAsync()
    {
        return (await _appHealthRepository.GetAllAppHealthsAsync()).ToList();
    }

    public async Task<List<AppHealth>> MonitorAppHealthsAsync()
    {
        var appHealths = await GetAppHealthsAsync();

        for (int i = 0; i < appHealths.Count; i++)
        {
            var appHealth = appHealths[i];

            if (!string.IsNullOrWhiteSpace(appHealth.Url) && appHealth.IsTimeToMonitor)
            {
                var method = GetHttpMethod(appHealth.Method);

                var healthModel = await _monitorClient.CheckHealthAsync(appHealth.Url, method);

                appHealth.Status = HealthStatus.Unhealthy;

                if (Enum.TryParse(typeof(HealthStatus),
                    healthModel.Status,
                    out var newStatus))
                {
                    appHealth.Status = (HealthStatus)newStatus;
                }
            }

            await UpdateAppHealthAsync(appHealth);
        }

        return appHealths;
    }

    public async Task<AppHealth> GetAppHealthByNameAsync(string name)
    {
        return await _appHealthRepository.GetAppHealthByNameAsync(name);
    }

    public async Task<AppHealth> AddAppHealthAsync(AppHealth newAppHealth)
    {
        PrepareToSave(newAppHealth);
        return await _appHealthRepository.CreateAppHealthAsync(newAppHealth);
    }

    public async Task<AppHealth> UpdateAppHealthAsync(AppHealth updatedAppHealth)
    {
        PrepareToSave(updatedAppHealth);
        return await _appHealthRepository.UpdateAppHealthAsync(updatedAppHealth);
    }

    public async Task RemoveAppHealthAsync(Guid id)
    {
        await _appHealthRepository.RemoveAppHealthAsync(id);
    }

    private void PrepareToSave(AppHealth appHealth)
    {
        appHealth.Method = GetHttpMethod(appHealth.Method).Method;
        appHealth.LastUpdateTime = DateTime.UtcNow;
    }

    private HttpMethod GetHttpMethod(string method)
    {
        switch (method?.ToUpper())
        {
            case "POST":
                return HttpMethod.Post;
            case "PUT":
                return HttpMethod.Put;
            case "DELETE":
                return HttpMethod.Delete;
            default:
                return HttpMethod.Get;
        }
    }
}
