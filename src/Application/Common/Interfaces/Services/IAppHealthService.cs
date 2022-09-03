using Defender.HealthMonitor.Application.DTOs;
using Defender.HealthMonitor.Domain.Entities.AppHealth;

namespace Defender.HealthMonitor.Application.Common.Interfaces;

public interface IAppHealthService
{
    public Task<List<AppHealth>> GetAppHealthsAsync();
    public Task<List<AppHealth>> MonitorAppHealthsAsync();

    public Task<AppHealth> GetAppHealthByNameAsync(string name);
    public Task<AppHealth> AddAppHealthAsync(AppHealth newAppHealth);
    public Task<AppHealth> UpdateAppHealthAsync(AppHealth updatedAppHealth);
    public Task RemoveAppHealthAsync(Guid id);
}