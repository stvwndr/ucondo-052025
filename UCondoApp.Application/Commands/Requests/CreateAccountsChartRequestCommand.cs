using UCondoApp.Application.Commands.Responses;
using static UCondoApp.Domain.Enums.UCondoAppDomainEnum;

namespace UCondoApp.Application.Commands.Requests;

public class CreateAccountsChartRequestCommand : BaseCommand<CreateAccountChartResponseCommand?>
{
    public Guid? ParentAccountId { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public AccountsChartType? AccountType { get; set; }
    public bool AcceptsReleases { get; set; }
}
