using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Defender.HealthMonitor.Application.Modules.Auth.Commands;
using Defender.HealthMonitor.Application.DTOs;
using Defender.HealthMonitor.WebUI.Attributes;
using Defender.HealthMonitor.Domain.Models;

namespace Defender.HealthMonitor.WebUI.Controllers.V1;

public class AccountInfoController : BaseApiController
{
    public AccountInfoController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpPut("update")]
    [Auth(Roles.Any)]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<UserDto> UpdateAccountInfoAsync(
        [FromBody] UpdateAccountInfoCommand command)
    {
        return await ProcessApiCallWithoutMappingAsync<UpdateAccountInfoCommand, UserDto>
            (command);
    }
}
