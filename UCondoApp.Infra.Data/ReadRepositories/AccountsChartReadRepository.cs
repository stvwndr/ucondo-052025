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
                a.FormattedCode,
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
                a.FormattedCode,
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
                a.FormattedCode,
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
                a.FormattedCode,
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

    public async Task<AccountsChartDto?> GetMaxCode()
    {
        var sql = @"
            SELECT 
                MAX(a.FormattedCode) AS FormattedCode
            FROM 
                AccountsChart a";

        var response = await DbConnection.QueryFirstOrDefaultAsync<AccountsChartDto>(sql);

        return !string.IsNullOrWhiteSpace(response?.FormattedCode)
            ? response
            : default;
    }

    public async Task<IList<AccountsChartDto>> GetMaxCode(string parentCode, string rootCode)
    {
        var sql = @"
            SELECT 
                MAX(a.FormattedCode) AS FormattedCode
            FROM 
                AccountsChart a
            WHERE
                a.Code LIKE @ParentCode
            UNION
            SELECT 
                MAX(a.FormattedCode) AS FormattedCode
            FROM 
                AccountsChart a
            WHERE
                a.Code LIKE @RootCode";

        var query = await DbConnection.QueryAsync<AccountsChartDto>(sql,
            new
            {
                ParentCode = $"{parentCode}%",
                RootCode = $"{rootCode}%"
            });

        return query
            .Where(a => !string.IsNullOrWhiteSpace(a.FormattedCode))
            .ToList();
    }

}
