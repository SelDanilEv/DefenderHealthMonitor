using Defender.HealthMonitor.Application.Common.Interfaces.Repositories;
using Defender.HealthMonitor.Application.Configuration.Options;
using Defender.HealthMonitor.Application.DTOs;
using Defender.HealthMonitor.Domain.Entities.AppHealth;
using Microsoft.Extensions.Options;

namespace Defender.HealthMonitor.Infrastructure.Repositories.Sample;

public class AppHealthRepository : MongoRepository<AppHealth>, IAppHealthRepository
{
    public AppHealthRepository(IOptions<MongoDbOption> mongoOption) : base(mongoOption.Value)
    {
    }

    public async Task<IList<AppHealth>> GetAllAppHealthsAsync()
    {
        return await GetItemsAsync();
    }

    public async Task<AppHealth> GetAppHealthByIdAsync(Guid id)
    {
        return await GetItemAsync(id);
    }

    public async Task<AppHealth> GetAppHealthByNameAsync(string name)
    {
        var filter = CreateFilterDefinition(ah => ah.Name == name);

        var items = await GetItemsWithFilterAsync(filter);

        return items.FirstOrDefault() ?? new AppHealth();
    }

    public async Task<AppHealth> CreateAppHealthAsync(AppHealth sample)
    {
        return await AddItemAsync(sample);
    }

    public async Task<AppHealth> UpdateAppHealthAsync(AppHealth updatedSample)
    {
        return await UpdateItemAsync(updatedSample);
    }

    public async Task RemoveAppHealthAsync(Guid id)
    {
        await RemoveItemAsync(id);
    }
}
