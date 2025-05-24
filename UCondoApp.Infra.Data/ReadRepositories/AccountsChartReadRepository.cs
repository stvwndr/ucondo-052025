using Dapper;
using Microsoft.EntityFrameworkCore;
using UCondoApp.CrossCutting.Dtos;
using UCondoApp.Infra.Data.Contexts;
using UCondoApp.Infra.Data.Interfaces.ReadRepository;

namespace UCondoApp.Infra.Data.ReadRepositories;

public class AccountsChartReadRepository : BaseReadRepository, IAccountsChartReadRepository
{
    public AccountsChartReadRepository(AccountsChartDbContext context)
        : base(context.Database.GetDbConnection()) { }

    public async Task<bool> AnyById(Guid id)
    {
        return (await GetById(id)) != null;
    }
    public async Task<AccountsChartDto?> GetById(Guid id)
    {
        var sql = @"
            SELECT 
                a.AccountsChartId AS Id,
                a.ParentAccountId,
                a.Code,
                a.Name,
                a.AccountType,
                a.AcceptsReleases
            FROM 
                AccountsChart a
            WHERE 
                a.AccountsChartId = @Id";

        var response = await DbConnection.QueryFirstOrDefaultAsync<AccountsChartDto>(sql,
            new
            {
                id
            });

        return response;
    }

    public async Task<IList<AccountsChartDto>> GetAll()
    {
        var sql = @"
            SELECT 
                a.AccountsChartId AS Id,
                a.ParentAccountId,
                a.Code,
                a.Name,
                a.AccountType,
                a.AcceptsReleases
            FROM 
                AccountsChart a";

        var query = await DbConnection.QueryAsync<AccountsChartDto>(sql);

        return query
            .ToList();
    }

    public async Task<IList<AccountsChartDto>> GetAllByPartialName(string partialName)
    {
        var sql = @"
            SELECT 
                a.AccountsChartId AS Id,
                a.ParentAccountId,
                a.Code,
                a.Name,
                a.AccountType,
                a.AcceptsReleases
            FROM 
                AccountsChart a
            WHERE
                a.Code ILIKE @Filter
                OR a.Name ILIKE @Filter";

        var query = await DbConnection.QueryAsync<AccountsChartDto>(sql,
            new 
            {
                Filter = $"{partialName}%"
            });

        return query
            .ToList();
    }

    public async Task<AccountsChartDto?> GetByCode(string code)
    {
        var sql = @"
            SELECT 
                a.AccountsChartId AS Id,
                a.ParentAccountId,
                a.Code,
                a.Name,
                a.AccountType,
                a.AcceptsReleases
            FROM 
                AccountsChart a
            WHERE
                a.Code = @Code";

        var response = await DbConnection.QueryFirstOrDefaultAsync<AccountsChartDto>(sql,
            new
            {
                code
            });

        return response;
    }
}
