using Defender.HealthMonitor.Application.Common.Interfaces;
using FluentValidation;
using MediatR;

using AppHealthModel = Defender.HealthMonitor.Domain.Entities.AppHealth.AppHealth;

namespace Defender.HealthMonitor.Application.Modules.AppHealth.Commands;

public record MonitorAppHealthCommand : IRequest<List<AppHealthModel>>
{
};

public sealed class MonitorAppHealthCommandValidator : AbstractValidator<MonitorAppHealthCommand>
{
    public MonitorAppHealthCommandValidator()
    {
    }
}

public sealed class MonitorAppHealthCommandHandler : IRequestHandler<MonitorAppHealthCommand, List<AppHealthModel>>
{
    private readonly IAppHealthService _appHealthService;

    public MonitorAppHealthCommandHandler(
        IAppHealthService appHealthService)
    {
        _appHealthService = appHealthService;
    }

    public async Task<List<AppHealthModel>> Handle(MonitorAppHealthCommand request, CancellationToken cancellationToken)
    {
        return await _appHealthService.MonitorAppHealthsAsync();
    }
}
