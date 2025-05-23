using AutoMapper;
using MediatR;
using UCondoApp.Application.Queries.Requests;
using UCondoApp.Application.Queries.Responses;
using UCondoApp.Domain.Services.Notifications.Interfaces;
using UCondoApp.Infra.Data.Interfaces.ReadRepository;

namespace UCondoApp.Application.Handlers;

public class AccountsChartQueryHandler : BaseCommandHandler,
    IRequestHandler<GetAllAccountsChartRequestQuery, IList<GetAllAccountsChartResponseQuery>>,
    IRequestHandler<GetAllAccountsChartsByPartialNameRequestQuery, IList<GetAllAccountsChartResponseQuery>>
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
}
