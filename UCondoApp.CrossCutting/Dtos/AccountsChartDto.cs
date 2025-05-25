using static UCondoApp.Domain.Enums.UCondoAppDomainEnum;

namespace UCondoApp.CrossCutting.Dtos;

public class AccountsChartDto
{
    public Guid? Id { get; set; }
    public Guid? ParentAccountId { get; set; }
    public string? Code { get; set; }
    public string? FormattedCode { get; set; }
    public string? Name { get; set; }
    public AccountsChartType AccountType { get; set; }
    public bool AcceptsReleases { get; set; }
}
