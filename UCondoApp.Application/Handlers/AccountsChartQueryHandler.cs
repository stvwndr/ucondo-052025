using AutoMapper;
using MediatR;
using UCondoApp.Application.Queries.Requests;
using UCondoApp.Application.Queries.Responses;
using UCondoApp.CrossCutting.Dtos;
using UCondoApp.Domain.Entitites;
using UCondoApp.Domain.Services.Notifications.Interfaces;
using UCondoApp.Infra.Data.Interfaces.ReadRepository;

namespace UCondoApp.Application.Handlers;

public class AccountsChartQueryHandler : BaseCommandHandler,
    IRequestHandler<GetAllAccountsChartRequestQuery, IList<GetAllAccountsChartResponseQuery>>,
    IRequestHandler<GetAllAccountsChartsByPartialNameRequestQuery, IList<GetAllAccountsChartResponseQuery>>,
    IRequestHandler<GetNextCodeRequestQuery, GetNextCodeResponseQuery?>
{
    private readonly IAccountsChartReadRepository _accountsChartReadRepository;
    private readonly IMapper _mapper;
    public AccountsChartQueryHandler(
        INotificationHandler notificationHandler,
        IAccountsChartReadRepository accountsChartReadRepository,
        IMapper mapper) 
        : base(notificationHandler)
    {
        _accountsChartReadRepository = accountsChartReadRepository;
        _mapper = mapper;
    }

    public async Task<IList<GetAllAccountsChartResponseQuery>> Handle(GetAllAccountsChartRequestQuery request, CancellationToken cancellationToken)
    {
        var accountList = await _accountsChartReadRepository.GetAll();

        return accountList
            .Select(_mapper.Map<GetAllAccountsChartResponseQuery>)
            .ToList();
    }

    public async Task<IList<GetAllAccountsChartResponseQuery>> Handle(GetAllAccountsChartsByPartialNameRequestQuery request, CancellationToken cancellationToken)
    {
        var accountList = string.IsNullOrWhiteSpace(request.PartialName)
            ? await _accountsChartReadRepository.GetAll()
            : await _accountsChartReadRepository.GetAllByPartialName(request.PartialName!);

        return accountList
            .Select(_mapper.Map<GetAllAccountsChartResponseQuery>)
            .ToList();
    }

    public async Task<GetNextCodeResponseQuery?> Handle(GetNextCodeRequestQuery request, CancellationToken cancellationToken)
    {
        AccountsChartDto? currentMaxAccount = null;
        if (string.IsNullOrWhiteSpace(request.ParentCode))
        {
            currentMaxAccount = await _accountsChartReadRepository.GetMaxCode();
        }
        else
        {
            var rootCode = AccountsChart.ExtractRootCode(request.ParentCode);
            var accountList = await _accountsChartReadRepository.GetMaxCode(parentCode: request.ParentCode, rootCode: rootCode);

            currentMaxAccount = accountList.FirstOrDefault();
            if (currentMaxAccount == null)
            {
                AddError(AccountsChart.Messages.AccountsChartParentCodeNotFound(request.ParentCode));
                return default;
            }

            if (currentMaxAccount.FormattedCode!.EndsWith("999"))
            {
                currentMaxAccount = accountList.Last();
            }
        }


        return currentMaxAccount == null
            ? new GetNextCodeResponseQuery(code: "1")
            : new GetNextCodeResponseQuery(code: AccountsChart.RetrieveNextCode(currentMaxAccount.FormattedCode!));
    }
}
