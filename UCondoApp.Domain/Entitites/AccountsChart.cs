using System.Net;
using System.Runtime.CompilerServices;
using static UCondoApp.Domain.Enums.UCondoAppDomainEnum;

namespace UCondoApp.Domain.Entitites;

public class AccountsChart : BaseEntity
{
    public Guid? ParentAccountId { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public AccountsChartType AccountType { get; set; }
    public bool AcceptsReleases { get; set; }

    public AccountsChart ParentAccount { get; set; }


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


    public static string? GetParentCode(string code)
    {
        var splited = code!.Split(".");
        var parentCode = splited.Length > 1
            ? string.Join(".", splited, 0, splited.Length - 1)
            : default;

        return parentCode;
    }

    public static class Messages
    {
        public static string AccountsChartNotFoundMessage(Guid id) => $"O plano de contas com ID {id} não foi encontrado.";
        public static string AccountsChartParentAcceptsReleasesMessage(string code) => $"A conta com código {code} aceita lançamentos, por isso não pode ser pai da conta que está sendo cadastrada.";
        public static string AccountsChartAlreadyExistsMessage(string code) => $"Já existe uma conta com código {code}.";
        public static string AccountsChartTypeMustBeTheSameOfParentMessage(string type) => $"O tipo de conta deve ser o mesmo da conta pai: {type}.";
    }
}
