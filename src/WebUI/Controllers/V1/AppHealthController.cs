using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Defender.HealthMonitor.Domain.Entities.AppHealth;
using Defender.HealthMonitor.Application.Modules.AppHealth.Commands;
using Defender.HealthMonitor.Application.Modules.AppHealth.Queries;
using Defender.HealthMonitor.Application.DTOs;
using Defender.HealthMonitor.WebUI.Attributes;
using Defender.HealthMonitor.Domain.Models;

namespace Defender.HealthMonitor.WebUI.Controllers.V1;

public class AppHealthController : BaseApiController
{
    public AppHealthController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpGet("statuses")]
    [Auth(Roles.SuperAdmin,Roles.Admin)]
    [ProducesResponseType(typeof(List<AppHealthDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<List<AppHealthDto>> GetAppHealthesAsync()
    {
        var command = new GetAppHealthQuery();

        return await ProcessApiCallAsync<GetAppHealthQuery, List<AppHealthDto>>
            (command);
    }

    [HttpGet("monitor")]
    [Auth(Roles.SuperAdmin)]
    [ProducesResponseType(typeof(List<AppHealthDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<List<AppHealthDto>> MonitorAppHealthesAsync()
    {
        var command = new MonitorAppHealthCommand();

        return await ProcessApiCallAsync<MonitorAppHealthCommand, List<AppHealthDto>>
            (command);
    }

    [HttpPost("create")]
    [Auth(Roles.SuperAdmin)]
    [ProducesResponseType(typeof(AppHealthDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<AppHealthDto> CreateAppHealthAsync(
        [FromBody] AppHealth appHealth)
    {
        var command = new CreateAppHealthCommand { AppHealth = appHealth };

        return await ProcessApiCallAsync<CreateAppHealthCommand, AppHealthDto>
            (command);
    }

    [HttpPut("update")]
    [Auth(Roles.SuperAdmin)]
    [ProducesResponseType(typeof(AppHealthDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<AppHealthDto> UpdateAppHealthAsync(
        [FromBody] AppHealth appHealth)
    {
        var command = new UpdateAppHealthCommand { AppHealth = appHealth };

        return await ProcessApiCallAsync<UpdateAppHealthCommand, AppHealthDto>
            (command);
    }

    [HttpDelete("delete")]
    [Auth(Roles.SuperAdmin)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task UpdateAppHealthAsync(DeleteAppHealthCommand command)
    {
        await ProcessApiCallAsync<DeleteAppHealthCommand>
            (command);
    }
}
