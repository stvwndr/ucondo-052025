using UCondoApp.Application.Commands.Requests;
using UCondoApp.Domain.Entitites;

namespace UCondoApp.Application.Factories;

public static class AccountsChartFactory
{
    public static AccountsChart CreateAccountsChartEntityFactory(
            CreateAccountsChartRequestCommand command)
    {
        var account =  new AccountsChart(
            parentAccountId: command.ParentAccountId,
            code: command.Code!,
            name: command.Name!,
            accountType: command.AccountType!.Value,
            acceptsReleases: command.AcceptsReleases);

        account.Id = Guid.NewGuid();

        return account;
    }
}
