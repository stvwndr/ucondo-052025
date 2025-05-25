using FluentValidation;
using UCondoApp.Application.Commands.Responses;
using UCondoApp.Domain.Entitites;
using static UCondoApp.Domain.Enums.UCondoAppDomainEnum;

namespace UCondoApp.Application.Commands.Requests;

public class CreateAccountsChartRequestCommand : BaseCommand<CreateAccountChartResponseCommand?>
{
    public Guid? ParentAccountId { get; set; }
    public string? Code { get; set; }
    public string? ParentCode { get; set; }
    public string? Name { get; set; }
    public AccountsChartType? AccountType { get; set; }
    public bool AcceptsReleases { get; set; }
}

public class CreateAccountsChartRequestCommandValidator : AbstractValidator<CreateAccountsChartRequestCommand>
{
    public CreateAccountsChartRequestCommandValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(c => c.Code)
            .NotEmpty()
            .WithMessage(AccountsChart.Messages.AccountsChartCodeIsMandatory);
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage(AccountsChart.Messages.AccountsChartNameIsMandatory);
        RuleFor(c => c.AccountType)
            .NotEmpty()
            .WithMessage(AccountsChart.Messages.AccountsChartTypeIsMandatory);
    }
}
