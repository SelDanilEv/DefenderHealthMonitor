using AutoMapper;
using Defender.HealthMonitor.Application.Common.Exceptions;
using Defender.HealthMonitor.Application.Common.Interfaces;
using Defender.HealthMonitor.Application.Helpers;
using Defender.HealthMonitor.Application.Modules.Auth.Commands;
using Defender.HealthMonitor.Infrastructure.Clients.UserManagement;

namespace Defender.HealthMonitor.Infrastructure.Services;
public class AccountManagementService : IAccountManagementService
{
    private readonly IUserManagementClient _userManagementClient;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public AccountManagementService(
        IUserManagementClient userManagementClient,
        ICurrentUserService currentUserService,
        IMapper mapper)
    {
        _userManagementClient = userManagementClient;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public async Task<Application.DTOs.UserDto> UpdateUserAsync(
        UpdateAccountInfoCommand command)
    {
        var requestCommand = new UpdateAccountFromUserCommand
        {
            User = new User
            {
                Id = _currentUserService.User.Id,
                Name = command.Name,
                Email = command.Email,
            }
        };

        try
        {
            var updateResponse = await _userManagementClient.UserPUT2Async(requestCommand);
            return _mapper.Map<Application.DTOs.UserDto>(updateResponse);
        }
        catch (ApiException ex)
        {
            SimpleLogger.Log(ex);

            var message = ExceptionParser.GetValidationMessage(ex);

            throw new ExternalAPIException(message, ex);
        }
    }
}
