using Defender.HealthMonitor.Domain.Entities;
using Defender.HealthMonitor.Domain.Entities.AppHealth;

namespace Defender.HealthMonitor.Application.DTOs;

public class AppHealthDto : IBaseModel
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Url { get; set; }
    public string? Method { get; set; }
    public List<MonitorTime>? MonitorTimes { get; set; }
    public string? Status { get; set; }
    public DateTime LastUpdateDate { get; set; }

    public bool IsTimeToMonitor =>
        MonitorTimes != null && MonitorTimes.Any(x => x.IsTimeToMonitor());
}
