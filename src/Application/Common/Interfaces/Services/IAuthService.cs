using Defender.HealthMonitor.Application.Models.LoginResponse;

namespace Defender.HealthMonitor.Application.Common.Interfaces;

public interface IAuthService
{
    Task<LoginResponse> Authenticate(string token);
}
