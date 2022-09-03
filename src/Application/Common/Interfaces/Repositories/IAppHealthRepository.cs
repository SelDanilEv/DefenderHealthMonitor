using Defender.HealthMonitor.Application.DTOs;
using Defender.HealthMonitor.Domain.Entities.AppHealth;

namespace Defender.HealthMonitor.Application.Common.Interfaces.Repositories;

public interface IAppHealthRepository
{
    Task<IList<AppHealth>> GetAllAppHealthsAsync();
    Task<AppHealth> GetAppHealthByIdAsync(Guid id);
    Task<AppHealth> GetAppHealthByNameAsync(string name);
    Task<AppHealth> CreateAppHealthAsync(AppHealth sample);
    Task<AppHealth> UpdateAppHealthAsync(AppHealth updatedSample);
    Task RemoveAppHealthAsync(Guid id);
}
