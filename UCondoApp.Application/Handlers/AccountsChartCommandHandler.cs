using MediatR;
using UCondoApp.Application.Commands.Requests;
using UCondoApp.Application.Commands.Responses;
using UCondoApp.Application.Factories;
using UCondoApp.Domain.Entitites;
using UCondoApp.Domain.Services.Notifications.Interfaces;
using UCondoApp.Infra.Data.Interfaces.ReadRepository;
using UCondoApp.Infra.Data.Interfaces.Repositories;

namespace UCondoApp.Application.Handlers;

public class AccountsChartCommandHandler : BaseCommandHandler,
    IRequestHandler<CreateAccountsChartRequestCommand, CreateAccountChartResponseCommand?>,
    IRequestHandler<DeleteAccountsChartRequestCommand, DeleteAccountChartResponseCommand?>
{
    private readonly IAccountsChartReadRepository _accountsChartReadRepository;
    private readonly IAccountsChartRepository _accountsChartRepository;
    public AccountsChartCommandHandler(
        INotificationHandler notificationHandler,
        IAccountsChartReadRepository accountsChartReadRepository,
        IAccountsChartRepository accountsChartRepository) 
        : base(notificationHandler)
    {
        _accountsChartReadRepository = accountsChartReadRepository;
        _accountsChartRepository = accountsChartRepository;
    }

    public async Task<CreateAccountChartResponseCommand?> Handle(CreateAccountsChartRequestCommand request, CancellationToken cancellationToken)
    {
        //validações aqui
        //if (await _accountsChartReadRepository.AnyByIntegrationSettingCompanyAndModel((UniqueInvoiceSettingDto)request))
        //{
        //    AddError(Domain.Entities.InvoiceSetting.Error.InvoiceSettingAlreadyExistsWithParameters,
        //        request.FiscalIntegrationSettingId,
        //        request.CompanyId,
        //        request.InvoiceModel);
        //    return ValidationResult;
        //}

        var account = AccountsChartFactory.CreateAccountsChartEntityFactory(request);

        await _accountsChartRepository.InsertEntity(account);

        if (await Commit(_accountsChartRepository.UnitOfWork))
        {
            return new CreateAccountChartResponseCommand(id: account.Id);
        }

        return default;
    }

    public async Task<DeleteAccountChartResponseCommand?> Handle(DeleteAccountsChartRequestCommand request, CancellationToken cancellationToken)
    {
        if (!await _accountsChartReadRepository.AnyById(request.Id))
        {
            AddError(AccountsChart.Messages.AccountsChartNotFoundMessage(request.Id));
            return default;
        }
        

        await _accountsChartRepository.DeleteEntity(request.Id);

        if (await Commit(_accountsChartRepository.UnitOfWork))
        {
            return new DeleteAccountChartResponseCommand();
        }

        return default;
    }
}
