using AutoMapper;
using Defender.HealthMonitor.WebUI.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Defender.HealthMonitor.Application.Modules.Home.Queries;
using Defender.HealthMonitor.Application.Enums;
using Defender.HealthMonitor.Domain.Models;
using Defender.HealthMonitor.Application.DTOs;

namespace Defender.HealthMonitor.WebUI.Controllers.V1;

public class HomeController : BaseApiController
{
    public HomeController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpGet("health")]
    [ProducesResponseType(typeof(HealthDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<HealthDto> HealthCheckAsync()
    {
        return new HealthDto { Status = "Healthy" };
    }

    [HttpGet("authorization/check")]
    [Auth(Roles.Any)]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<object> AuthorizationCheckAsync()
    {
        return new { IsAuthorized = true };
    }

    [Auth(Roles.SuperAdmin)]
    [HttpGet("configuration")]
    [ProducesResponseType(typeof(Dictionary<string, string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<Dictionary<string, string>> GetConfigurationAsync(
        ConfigurationLevel configurationLevel)
    {
        var query = new GetConfigurationQuery()
        {
            Level = configurationLevel
        };

        return await ProcessApiCallWithoutMappingAsync
            <GetConfigurationQuery, Dictionary<string, string>>
            (query);
    }
}
