using Defender.HealthMonitor.Application.DTOs;
using Defender.HealthMonitor.Application.Modules.Auth.Commands;

namespace Defender.HealthMonitor.Application.Common.Interfaces;

public interface IAccountManagementService
{
    Task<UserDto> UpdateUserAsync(UpdateAccountInfoCommand command);
}
