using Microsoft.EntityFrameworkCore;
using UCondoApp.Infra.Data.UoW.Interfaces;

namespace UCondoApp.Infra.Data.Contexts;

public class AccountsChartDbContext : DbContext, IContextUnitOfWork
{
    public AccountsChartDbContext(DbContextOptions<AccountsChartDbContext> options)
        : base(options)
    {
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken) > 0;
    }
}