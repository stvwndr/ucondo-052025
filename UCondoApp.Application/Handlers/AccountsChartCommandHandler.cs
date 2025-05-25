using MediatR;
using UCondoApp.Application.Commands.Requests;
using UCondoApp.Application.Commands.Responses;
using UCondoApp.Application.Factories;
using UCondoApp.Application.Interfaces;
using UCondoApp.Domain.Services.Notifications.Interfaces;
using UCondoApp.Infra.Data.Interfaces.Repositories;

namespace UCondoApp.Application.Handlers;

public class AccountsChartCommandHandler : BaseCommandHandler,
    IRequestHandler<CreateAccountsChartRequestCommand, CreateAccountChartResponseCommand?>,
    IRequestHandler<DeleteAccountsChartRequestCommand, DeleteAccountChartResponseCommand?>
{    
    private readonly IAccountsChartRepository _accountsChartRepository;
    private readonly IAccountsChartService _accountsChartService;

    public AccountsChartCommandHandler(
        IAccountsChartRepository accountsChartRepository,
        IAccountsChartService accountsChartService,
        INotificationHandler notificationHandler)
        : base(notificationHandler)
    {
        _accountsChartRepository = accountsChartRepository;
        _accountsChartService = accountsChartService;
    }

    public async Task<CreateAccountChartResponseCommand?> Handle(CreateAccountsChartRequestCommand request, CancellationToken cancellationToken)
    {
        if (!await _accountsChartService.ValidateInsertOperation(request))
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
        if (!await _accountsChartService.ValidateDeleteOperation(request))
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
}
