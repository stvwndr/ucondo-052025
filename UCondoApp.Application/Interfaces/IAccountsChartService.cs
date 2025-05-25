using UCondoApp.Application.Commands.Requests;
using UCondoApp.CrossCutting.Dtos;

namespace UCondoApp.Application.Interfaces;

public interface IAccountsChartService
{
    Task<bool> ValidateDeleteOperation(DeleteAccountsChartRequestCommand request);
    Task<bool> ValidateInsertOperation(CreateAccountsChartRequestCommand request);
    Task<AccountsChartDto?> RetrieveParentAccount(CreateAccountsChartRequestCommand request);
}
