using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCondoApp.Infra.Data.Contexts;

namespace UCondoApp.Api.Extensions;

public static class MigrationExtensions
{
    public static IHost MigrateDatabase(this IHost host)
    {
        Task.Factory.StartNew(() =>
        {
            using var scope = host.Services.CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<AccountsChartDbContext>>();

            using var context = scope.ServiceProvider.GetRequiredService<AccountsChartDbContext>();
            try
            {
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                logger.LogError($"Erro ao tentar executar a migration: {ex.Message}");
                throw;
            }
        });

        return host;
    }
}
