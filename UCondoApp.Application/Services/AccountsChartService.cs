using UCondoApp.Application.Commands.Requests;
using UCondoApp.Application.Interfaces;
using UCondoApp.CrossCutting.Dtos;
using UCondoApp.Domain.Entitites;
using UCondoApp.Domain.Services.Notifications.Interfaces;
using UCondoApp.Infra.Data.Interfaces.ReadRepository;

namespace UCondoApp.Application.Services;

public class AccountsChartService : IAccountsChartService
{
    private readonly IAccountsChartReadRepository _accountsChartReadRepository;
    private readonly INotificationHandler _notificationHandler;

    public AccountsChartService(
        IAccountsChartReadRepository accountsChartReadRepository,
        INotificationHandler notificationHandler)
    {
        _accountsChartReadRepository = accountsChartReadRepository;
        _notificationHandler = notificationHandler;
    }

    public async Task<bool> ValidateDeleteOperation(DeleteAccountsChartRequestCommand request)
    {
        if (!await _accountsChartReadRepository.AnyById(request.Id))
        {
            _notificationHandler.AddNotification(AccountsChart.Messages.AccountsChartNotFoundMessage(request.Id));
            return false;
        }

        return true;
    }

    public async Task<bool> ValidateInsertOperation(CreateAccountsChartRequestCommand request)
    {
        var foundAccount = await _accountsChartReadRepository.GetByCode(request.Code!);
        if (foundAccount != null)
        {
            _notificationHandler.AddNotification(AccountsChart.Messages.AccountsChartAlreadyExistsMessage(request.Code!));
            return false;
        }

        var lastCode = request.Code!.Split(".").Last();
        if (!int.TryParse(lastCode, out var sufix))
        {
            _notificationHandler.AddNotification(AccountsChart.Messages.AccountsChartCodeIsInvalid(request.Code));
            return false;
        }
        else if (sufix > 999)
        {
            _notificationHandler.AddNotification(AccountsChart.Messages.AccountsChartCodeOutOfRange);
            return false;
        }


        var parentAccount = await RetrieveParentAccount(request);
        if (parentAccount == null)
        {
            if (request.ParentAccountId != null)
            {
                _notificationHandler.AddNotification(AccountsChart.Messages.AccountsChartParentIdNotFound(request.ParentAccountId.Value));
                return false;
            }
            else if (!string.IsNullOrWhiteSpace(request.ParentCode))
            {
                _notificationHandler.AddNotification(AccountsChart.Messages.AccountsChartParentCodeNotFound(request.ParentCode!));
                return false;
            }
        }
        else
        {
            request.ParentAccountId = parentAccount.Id;

            if (parentAccount.AcceptsReleases)
            {
                _notificationHandler.AddNotification(AccountsChart.Messages.AccountsChartParentAcceptsReleasesMessage(parentAccount.Code!));
                return false;
            }

            if (parentAccount.AccountType != request.AccountType)
            {
                _notificationHandler.AddNotification(AccountsChart.Messages.AccountsChartTypeMustBeTheSameOfParentMessage(parentAccount.AccountType.ToString("G")));
                return false;
            }

            var parentCode = AccountsChart.ExtractParentCode(request.Code);
            if (!parentAccount.Code!.Equals(parentCode))
            {
                _notificationHandler.AddNotification(AccountsChart.Messages.AccountsChartCodeMustBeTheSameOfParentMessage);
                return false;
            }
        }


        return true;
    }

    public async Task<AccountsChartDto?> RetrieveParentAccount(CreateAccountsChartRequestCommand request)
    {
        AccountsChartDto? parentAccount = default;
        if (request.ParentAccountId != null)
        {
            parentAccount = await _accountsChartReadRepository.GetById(request.ParentAccountId.Value);
        }
        else if (!string.IsNullOrWhiteSpace(request.ParentCode))
        {
            parentAccount = await _accountsChartReadRepository.GetByCode(request.ParentCode!);
        }
        else
        {
            var parentCode = AccountsChart.ExtractParentCode(request.Code!);
            if (!string.IsNullOrWhiteSpace(parentCode))
            {
                parentAccount = await _accountsChartReadRepository.GetByCode(parentCode);
            }
        }

        return parentAccount;
    }
}
