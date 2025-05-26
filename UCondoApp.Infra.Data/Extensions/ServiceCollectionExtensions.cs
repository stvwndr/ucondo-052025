using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UCondoApp.Infra.Data.Contexts;
using UCondoApp.Infra.Data.Interfaces.ReadRepository;
using UCondoApp.Infra.Data.Interfaces.Repositories;
using UCondoApp.Infra.Data.ReadRepositories;
using UCondoApp.Infra.Data.Repositories;

namespace UCondoApp.Infra.Data.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfraData(this IServiceCollection services,
        IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));
        ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));

        services.AddDbContext<AccountsChartDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString(DefaultConnectionStringKeyName))
                .UseLowerCaseNamingConvention());


        services.AddScoped<IAccountsChartReadRepository, AccountsChartReadRepository>();
        services.AddScoped<IAccountsChartRepository, AccountsChartRepository>();


        return services;
    }


    public const string DefaultConnectionStringKeyName = "DefaultConnection";
}
