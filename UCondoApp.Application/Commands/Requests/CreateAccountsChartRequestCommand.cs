using System.ComponentModel.DataAnnotations;
using UCondoApp.Application.Commands.Responses;
using UCondoApp.Domain.Entitites;
using static UCondoApp.Domain.Enums.UCondoAppDomainEnum;

namespace UCondoApp.Application.Commands.Requests;

public class CreateAccountsChartRequestCommand : BaseCommand<CreateAccountChartResponseCommand?>
{
    public Guid? ParentAccountId { get; set; }
    [Required(ErrorMessage = AccountsChart.Messages.AccountsChartCodeIsMandatory)]
    public string? Code { get; set; }
    public string? ParentCode { get; set; }
    [Required(ErrorMessage = AccountsChart.Messages.AccountsChartNameIsMandatory)]
    public string? Name { get; set; }
    [Required(ErrorMessage = AccountsChart.Messages.AccountsChartTypeIsMandatory)]
    public AccountsChartType? AccountType { get; set; }
    public bool AcceptsReleases { get; set; }
}
