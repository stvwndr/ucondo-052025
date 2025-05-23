using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UCondoApp.Application.Mapping;

namespace UCondoApp.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.ConfigureMediatR();
        services.ConfigureAutoMapper();

        return services;
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
