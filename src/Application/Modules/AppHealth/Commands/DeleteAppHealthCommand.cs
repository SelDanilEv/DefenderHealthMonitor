using Defender.HealthMonitor.Application.Common.Interfaces;
using FluentValidation;
using MediatR;

namespace Defender.HealthMonitor.Application.Modules.AppHealth.Commands;

public record DeleteAppHealthCommand : IRequest
{
    public Guid Id { get; set; }
};

public sealed class DeleteAppHealthCommandValidator : AbstractValidator<DeleteAppHealthCommand>
{
    public DeleteAppHealthCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().WithMessage("No id!");
    }
}

public sealed class DeleteAppHealthCommandHandler : IRequestHandler<DeleteAppHealthCommand>
{
    private readonly IAppHealthService _appHealthService;

    public DeleteAppHealthCommandHandler(
        IAppHealthService appHealthService)
    {
        _appHealthService = appHealthService;
    }

    public async Task<Unit> Handle(DeleteAppHealthCommand request, CancellationToken cancellationToken)
    {
        await _appHealthService.RemoveAppHealthAsync(request.Id);

        return Unit.Value;
    }
}
