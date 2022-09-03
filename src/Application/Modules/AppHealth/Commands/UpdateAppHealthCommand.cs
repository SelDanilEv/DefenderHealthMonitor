using Defender.HealthMonitor.Application.Common.Interfaces;
using FluentValidation;
using MediatR;

using AppHealthModel = Defender.HealthMonitor.Domain.Entities.AppHealth.AppHealth;

namespace Defender.HealthMonitor.Application.Modules.AppHealth.Commands;

public record UpdateAppHealthCommand : IRequest<AppHealthModel>
{
    public AppHealthModel? AppHealth { get; set; }
};

public sealed class UpdateAppHealthCommandValidator : AbstractValidator<UpdateAppHealthCommand>
{
    public UpdateAppHealthCommandValidator()
    {
        RuleFor(x => x.AppHealth).NotNull().WithMessage("No value!");
        RuleFor(x => x.AppHealth.Id).NotNull().WithMessage("No id!");
        RuleFor(x => x.AppHealth.Name).NotNull().WithMessage("Please set name!");
        RuleFor(x => x.AppHealth.Url).NotNull().WithMessage("Please set url!");
        RuleFor(x => x.AppHealth.Method).NotNull().WithMessage("Please set name!");
    }
}

public sealed class UpdateAppHealthCommandHandler : IRequestHandler<UpdateAppHealthCommand, AppHealthModel>
{
    private readonly IAppHealthService _appHealthService;

    public UpdateAppHealthCommandHandler(
        IAppHealthService appHealthService)
    {
        _appHealthService = appHealthService;
    }

    public async Task<AppHealthModel> Handle(UpdateAppHealthCommand request, CancellationToken cancellationToken)
    {
        return await _appHealthService.UpdateAppHealthAsync(request.AppHealth);
    }
}
