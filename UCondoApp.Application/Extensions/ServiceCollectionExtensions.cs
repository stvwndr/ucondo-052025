using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UCondoApp.Application.Interfaces;
using UCondoApp.Application.Mapping;
using UCondoApp.Application.Services;
using UCondoApp.Domain.Services.Notifications;
using UCondoApp.Domain.Services.Notifications.Interfaces;

namespace UCondoApp.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.ConfigureDomain();
        services.ConfigureMediatR();
        services.ConfigureAutoMapper();

        services.AddScoped<IAccountsChartService, AccountsChartService>();

        return services;
    }

    private static void ConfigureDomain(this IServiceCollection services)
    {
        services.AddScoped<INotificationHandler, NotificationHandler>();
    }

    private static void ConfigureMediatR(this IServiceCollection services)
    {
        services.AddMediatR(delegate (MediatRServiceConfiguration cfg)
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
    }

    private static void ConfigureAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(UCondoApplicationMappingProfile));
    }
}
