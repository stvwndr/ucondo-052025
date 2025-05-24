using Microsoft.EntityFrameworkCore;
using UCondoApp.Infra.Data.Configurations;
using UCondoApp.Infra.Data.UoW.Interfaces;

namespace UCondoApp.Infra.Data.Contexts;

public class AccountsChartDbContext : DbContext, IContextUnitOfWork
{
    public AccountsChartDbContext(DbContextOptions<AccountsChartDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new AccountsChartEntityConfiguration());
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken) > 0;
    }
}