using Defender.HealthMonitor.Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace Defender.HealthMonitor.Domain.Entities.AppHealth;

public class AppHealth : IBaseModel
{
    [BsonId]
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Url { get; set; }
    public string? Method { get; set; }
    public List<MonitorTime>? MonitorTimes { get; set; }
    [BsonIgnoreIfNull]
    public HealthStatus? Status { get; set; }
    public DateTime LastUpdateTime { get; set; }

    public bool IsTimeToMonitor => 
        MonitorTimes != null && MonitorTimes.Any(x => x.IsTimeToMonitor());
}
