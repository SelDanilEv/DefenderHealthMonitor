using Defender.HealthMonitor.Application.Common.Interfaces;
using FluentValidation;
using MediatR;

using AppHealthModel = Defender.HealthMonitor.Domain.Entities.AppHealth.AppHealth;

namespace Defender.HealthMonitor.Application.Modules.AppHealth.Commands;

public record CreateAppHealthCommand : IRequest<AppHealthModel>
{
    public AppHealthModel? AppHealth { get; set; }
};

public sealed class CreateAppHealthCommandValidator : AbstractValidator<CreateAppHealthCommand>
{
    public CreateAppHealthCommandValidator()
    {
        RuleFor(x => x.AppHealth).NotNull().WithMessage("No value!");
        RuleFor(x => x.AppHealth.Name).NotNull().WithMessage("Please set name!");
        RuleFor(x => x.AppHealth.Url).NotNull().WithMessage("Please set url!");
        RuleFor(x => x.AppHealth.Method).NotNull().WithMessage("Please set name!");
    }
}

public sealed class CreateAppHealthCommandHandler : IRequestHandler<CreateAppHealthCommand, AppHealthModel>
{
    private readonly IAppHealthService _appHealthService;

    public CreateAppHealthCommandHandler(
        IAppHealthService appHealthService)
    {
        _appHealthService = appHealthService;
    }

    public async Task<AppHealthModel> Handle(CreateAppHealthCommand request, CancellationToken cancellationToken)
    {
        return await _appHealthService.AddAppHealthAsync(request.AppHealth);
    }
}
