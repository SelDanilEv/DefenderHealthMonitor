using AutoMapper;
using Defender.HealthMonitor.Application.Common.Interfaces;
using Defender.HealthMonitor.Infrastructure.Clients.UserManagement;

namespace Defender.HealthMonitor.Infrastructure.Services;
public class AuthService : IAuthService
{
    private readonly IUserManagementClient _userManagementClient;
    private readonly IMapper _mapper;

    public AuthService(
        IUserManagementClient userManagementClient,
        IMapper mapper)
    {
        _userManagementClient = userManagementClient;
        _mapper = mapper;
    }

    public async Task<Application.Models.LoginResponse.LoginResponse> Authenticate(string token)
    {
        var command = new LoginGoogleCommand { Token = token };

        var loginResponse = await _userManagementClient.GoogleAsync(command);

        return _mapper.Map<Application.Models.LoginResponse.LoginResponse>(loginResponse);
    }
}
