using static UCondoApp.Domain.Enums.UCondoAppDomainEnum;

namespace UCondoApp.Domain.Entitites;

public class AccountsChart : BaseEntity
{
    public Guid? ParentAccountId { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public AccountsChartType AccountType { get; set; }
    public bool AcceptsReleases { get; set; }


    public AccountsChart(
        Guid? parentAccountId,
        string code,
        string name,
        AccountsChartType accountType,
        bool acceptsReleases)
    {
        ParentAccountId = parentAccountId;
        Code = code;
        Name = name;
        AccountType = accountType;
        AcceptsReleases = acceptsReleases;
    }



    public static class Messages
    {
        public static string AccountsChartNotFoundMessage(Guid id) => $"O plano de contas com ID {id} não foi encontrado.";
    }
}
