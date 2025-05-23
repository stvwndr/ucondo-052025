using UCondoApp.Application.Commands;
using UCondoApp.Application.Queries.Responses;

namespace UCondoApp.Application.Queries.Requests;

public class GetAllAccountsChartsByPartialNameRequestQuery : BaseCommand<IList<GetAllAccountsChartResponseQuery>>
{
    public string? PartialName { get; set; }
}
