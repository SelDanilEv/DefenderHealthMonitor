using Defender.HealthMonitor.Application.Common.Interfaces;
using FluentValidation;
using MediatR;

using AppHealthModel = Defender.HealthMonitor.Domain.Entities.AppHealth.AppHealth;

namespace Defender.HealthMonitor.Application.Modules.AppHealth.Queries;

public record GetAppHealthQuery : IRequest<List<AppHealthModel>>
{
};

public sealed class GetAppHealthQueryValidator : AbstractValidator<GetAppHealthQuery>
{
    public GetAppHealthQueryValidator()
    {
    }
}

public sealed class GetAppHealthQueryHandler : IRequestHandler<GetAppHealthQuery, List<AppHealthModel>>
{
    private readonly IAppHealthService _appHealthService;

    public GetAppHealthQueryHandler(
        IAppHealthService appHealthService)
    {
        _appHealthService = appHealthService;
    }

    public async Task<List<AppHealthModel>> Handle(GetAppHealthQuery request, CancellationToken cancellationToken)
    {
        return await _appHealthService.GetAppHealthsAsync();
    }
}
