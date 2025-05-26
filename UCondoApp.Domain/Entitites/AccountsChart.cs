using System.Net;
using System.Runtime.CompilerServices;
using static UCondoApp.Domain.Enums.UCondoAppDomainEnum;

namespace UCondoApp.Domain.Entitites;

public class AccountsChart : BaseEntity
{
    public Guid? ParentAccountId { get; private set; }
    public string Code { get; private set; }
    public string FormattedCode { get; private set; }
    public string Name { get; private set; }
    public AccountsChartType AccountType { get; private set; }
    public bool AcceptsReleases { get; private set; }

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

        FormattedCode = RetrieveFormattedCode(Code)!;
    }

    public static string? RetrieveFormattedCode(string code)
    {
        var splited = code.Split('.');
        var formatted = splited.Select(s => s.PadLeft(3, '0'));
        
        return string.Join(".", formatted);
    }

    public static string? ExtractParentCode(string code)
    {
        var splited = code!.Split(".");
        var parentCode = splited.Length > 1
            ? string.Join(".", splited, 0, splited.Length - 1)
            : default;

        return parentCode;
    }

    public static string ExtractRootCode(string code)
    {
        var rootCode = ExtractParentCode(code);
        if (string.IsNullOrWhiteSpace(rootCode)) return code;
        
        while (!string.IsNullOrWhiteSpace(rootCode) && rootCode!.EndsWith("999"))
        {
            var splited = rootCode.Split(".");
            var newCode = string.Join(".", splited, 0, splited.Length - 1);

            rootCode = ExtractParentCode(newCode);
        }
        
        return rootCode ?? "999";
    }

    public static string RetrieveNextCode(string currentCode)
    {
        var nextCode = currentCode.EndsWith("999")
            ? AccountsChart.ExtractRootCode(currentCode)
            : currentCode;

        var splited = nextCode.Split(".");
        var code = int.Parse(splited.Last());
        splited[splited.Length - 1] = (++code).ToString();

        var numericSplited = splited.Select(int.Parse);
        nextCode = string.Join(".", numericSplited);

        return nextCode;
    }

    public static class Messages
    {
        public static string AccountsChartNotFoundMessage(Guid id) => $"O plano de contas com ID {id} não foi encontrado.";
        public static string AccountsChartParentAcceptsReleasesMessage(string code) => $"A conta com código {code} aceita lançamentos, por isso não pode ser pai da conta que está sendo cadastrada.";
        public static string AccountsChartAlreadyExistsMessage(string code) => $"Já existe uma conta com código {code}.";
        public static string AccountsChartTypeMustBeTheSameOfParentMessage(string type) => $"O tipo de conta deve ser o mesmo da conta pai: {type}.";
        public static string AccountsChartParentCodeNotFound(string code) => $"Não foi encontrada conta pai com o código {code} na base de dados.";
        public static string AccountsChartParentIdNotFound(Guid id) => $"Não foi encontrada conta pai com ID {id} na base de dados.";
        public static string AccountsChartCodeIsInvalid(string code) => $"O código {code} não é um número válido.";

        public const string AccountsChartCodeIsMandatory = "O código da conta deve ser informado.";
        public const string AccountsChartNameIsMandatory = "O nome da conta deve ser informado.";
        public const string AccountsChartTypeIsMandatory = "O tipo da conta deve ser informado.";
        public const string AccountsChartCodeOutOfRange = $"O código deve ser menor ou igual a '999'.";
        public const string AccountsChartCodeMustBeTheSameOfParentMessage = $"O código da conta deve ter o mesmo prefixo da conta pai.";
    }
}
