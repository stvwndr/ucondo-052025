using UCondoApp.Infra.Data.Contexts;
using UCondoApp.Infra.Data.Interfaces.Repositories;

namespace UCondoApp.Infra.Data.Repositories;

public class AccountsChartRepository : BaseRepository<Domain.Entitites.AccountsChart>,
    IAccountsChartRepository
{
    public AccountsChartRepository(AccountsChartDbContext context)
        : base(context)
    {
    }
}
