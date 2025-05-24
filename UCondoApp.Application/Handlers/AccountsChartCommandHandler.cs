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
        if (!await ValidateInsertOperation(request))
        {
            return default;
        }


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
        if (!await ValidateDeleteOperation(request))
        {
            return default;
        }


        await _accountsChartRepository.DeleteEntity(request.Id);

        if (await Commit(_accountsChartRepository.UnitOfWork))
        {
            return new DeleteAccountChartResponseCommand();
        }

        return default;
    }

    private async Task<bool> ValidateDeleteOperation(DeleteAccountsChartRequestCommand request)
    {
        if (!await _accountsChartReadRepository.AnyById(request.Id))
        {
            AddError(AccountsChart.Messages.AccountsChartNotFoundMessage(request.Id));
            return false;
        }

        return true;
    }

    private async Task<bool> ValidateInsertOperation(CreateAccountsChartRequestCommand request)
    {
        var foundAccount = await _accountsChartReadRepository.GetByCode(request.Code!);
        if (foundAccount != null)
        {
            AddError(AccountsChart.Messages.AccountsChartAlreadyExistsMessage(request.Code!));
            return false;
        }


        var parentCode = AccountsChart.GetParentCode(request.Code!);
        var parentAccount = parentCode != null
            ? await _accountsChartReadRepository.GetByCode(parentCode)
            : default;

        if (parentAccount != null && parentAccount.AcceptsReleases)
        {
            AddError(AccountsChart.Messages.AccountsChartParentAcceptsReleasesMessage(parentCode!));
            return false;
        }

        if (parentAccount != null && parentAccount.AccountType != request.AccountType)
        {
            AddError(AccountsChart.Messages.AccountsChartTypeMustBeTheSameOfParentMessage(parentAccount.AccountType.ToString("G")));
            return false;
        }

        return true;
    }
}
