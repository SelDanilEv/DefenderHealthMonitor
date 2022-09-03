namespace Defender.HealthMonitor.Infrastructure.Clients.Monitor;

public interface IMonitorClient
{
    Task<HealthModel> CheckHealthAsync(string url, HttpMethod method);
}
