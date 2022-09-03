using System.Net.Http.Headers;
using System.Reflection;
using Defender.HealthMonitor.Application.Common.Interfaces;
using Defender.HealthMonitor.Application.Common.Interfaces.Repositories;
using Defender.HealthMonitor.Application.Configuration.Options;
using Defender.HealthMonitor.Infrastructure.Clients;
using Defender.HealthMonitor.Infrastructure.Clients.Monitor;
using Defender.HealthMonitor.Infrastructure.Clients.UserManagement;
using Defender.HealthMonitor.Infrastructure.Repositories.Sample;
using Defender.HealthMonitor.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Defender.HealthMonitor.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.RegisterServices();

        services.RegisterRepositories();

        services.RegisterApiClients();

        return services;
    }

    private static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<IAppHealthService, AppHealthService>();
        services.AddTransient<IAccountManagementService, AccountManagementService>();

        return services;
    }

    private static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddTransient<IAppHealthRepository, AppHealthRepository>();

        return services;
    }

    private static IServiceCollection RegisterApiClients(
        this IServiceCollection services)
    {
        services.AddHttpClient<IUserManagementClient, UserManagementClient>("UserManagementClient",
            (serviceProvider, client) =>
        {
            client.BaseAddress = new Uri(
                serviceProvider.GetRequiredService<IOptions<UserManagementOption>>().Value.Url);

            var token = serviceProvider.GetRequiredService<ICurrentUserService>().Token;

            if (!string.IsNullOrWhiteSpace(token))
            {
                client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(token);
            }
        });


        services.AddHttpClient<IMonitorClient, MonitorClient>("UserManagementClient");

        return services;
    }

}
