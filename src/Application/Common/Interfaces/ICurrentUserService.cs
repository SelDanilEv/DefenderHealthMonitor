using Defender.HealthMonitor.Domain.Entities.User;

namespace Defender.HealthMonitor.Application.Common.Interfaces;

public interface ICurrentUserService
{
    User? User { get; }
    string Token { get; }
}
